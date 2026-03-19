# 1. Create two functions to update all usages for:
---
Old: `Console.WriteLine("[WARNING]: ....")`  
New: `WriteWarningMessage(string message)` 

Old: `Console.WriteLine("[ERROR]: ....")`
New: `WriteErrorMessage(string message, int? exitCode, bool exit = false)` 

---

