@echo off
echo "%SITE_FLAVOR%"
    IF "%SITE_FLAVOR%" == "nodejs" (
      deploynode.cmd
    ) ELSE (
      IF "%SITE_FLAVOR%" == "wapi" (
        deploywebapi.cmd
      ) ELSE (
        echo You have to set SITE_FLAVOR setting to either "nodejs" or "mvc4"
        exit /b 1
      )
    )