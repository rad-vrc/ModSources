@echo off
echo === TranslateTest2 ALL_TEXT_ja.txt Translation Tool ===
echo.

REM Check if python is available
python --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: Python is not installed or not in PATH
    echo Please install Python 3.7 or later
    pause
    exit /b 1
)

REM Check if input file exists
if not exist "References\ALL_TEXT_ja.txt" (
    echo ERROR: Input file not found: References\ALL_TEXT_ja.txt
    echo Please ensure you're running from the project root directory
    pause
    exit /b 1
)

echo Running Python translation script...
echo.

python scripts\translate_all_text.py

echo.
echo Translation process completed.
pause