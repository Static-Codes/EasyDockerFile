import os
import requests
import xml.etree.ElementTree as ET
from pathlib import Path

# Github runner notes
# ::error:: -> Displays as an error
# ::notice:: -> Blue/gray test to denote importance but not error

def update_images_xml():
    xml_path = Path(os.getenv("IMAGES_XML_PATH", "Images.xml"))

    try:
        print("Fetching Fedora release data")
        resp = requests.get("https://fedoraproject.org/releases.json")
        resp.raise_for_status()
        data = resp.json()
        print("::notice::Fetch operation successful")
    except Exception as e:
        print(f"::error::Failed to fetch fedora release data: {e}")
        return

    # Determines the latest two numeric versions of Fedora
    all_versions = sorted(
        list(set(item['version'] for item in data if item['version'].isdigit())), 
        key=int, 
        reverse=True
    )

    latest_fedora_versions = all_versions[:2]

    arch_map = {
        "x86_64": "x64",
        "aarch64": "arm64",
        "armhf": "armhf"
    }

    try:
        print("Parsing Images.xml")
        tree = ET.parse(xml_path)
        root = tree.getroot()
        print("::notice::Successfully parsed Images.xml")
    except FileNotFoundError as e:
        print(f"::error::The path {xml_path} does not exist, please check IMAGES_XML_PATH.")
        return

    print("Determining latest fedora releases")

    # Locates the Fedora family node
    fedora_family = next((f for f in root.findall('family') if "Fedora" in (f.text or "")), None)

    if fedora_family is not None:

        # Clears existing images
        for img in fedora_family.findall('image'):
            fedora_family.remove(img)

        # Builds new image nodes
        for version in latest_fedora_versions:
            raw_arches = set(item['arch'] for item in data if item['version'] == version)
            mapped_arches = sorted([arch_map[arch] for arch in raw_arches if arch in arch_map])

            img_node = ET.SubElement(fedora_family, "image")
            ET.SubElement(img_node, "version").text = f"{version}.0"
            ET.SubElement(img_node, "full_name").text = f"Fedora {version}.0"
            
            arch_parent = ET.SubElement(img_node, "supported_architectures")
            for mapped_arch in mapped_arches:
                ET.SubElement(arch_parent, "supported_architecture").text = mapped_arch
    
    print("::notice::Determined latest fedora releases")


    print("Updating Images.xml")
    
    # Applies a two space indent for each tree.
    ET.indent(tree, space="  ", level=0)
    
    # <?xml declaration is not needed here.
    tree.write(xml_path, encoding='utf-8', xml_declaration=False)
    print(f"::notice::Successfully updated Fedora release data in {xml_path}")

if __name__ == "__main__":
    update_images_xml()