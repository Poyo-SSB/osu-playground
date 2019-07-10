@echo off
set /p version="Version tag (v1.x.x): "

echo Building...

"C:\Program Files\Unity\Editor\Unity" -batchmode -quit -projectPath %cd% -executeMethod ApplicationBuilder.Build -version %version% -buildfolder %cd%\Builds > nul
echo Copying scripts...

robocopy /s /mir %cd%\Scripts\ %cd%\Builds\%version%\osuPlayground-%version%-win32\Scripts\ > nul
robocopy /s /mir %cd%\Scripts\ %cd%\Builds\%version%\osuPlayground-%version%-win64\Scripts\ > nul
robocopy /s /mir %cd%\Scripts\ %cd%\Builds\%version%\osuPlayground-%version%-mac\Scripts\ > nul
robocopy /s /mir %cd%\Scripts\ %cd%\Builds\%version%\osuPlayground-%version%-linux\Scripts\ > nul

echo Zipping...

"C:\Program Files\7-Zip\7z.exe" a %cd%\Builds\%version%\osuPlayground-%version%-win32.zip %cd%\Builds\%version%\osuPlayground-%version%-win32\* > nul
"C:\Program Files\7-Zip\7z.exe" a %cd%\Builds\%version%\osuPlayground-%version%-win64.zip %cd%\Builds\%version%\osuPlayground-%version%-win64\* > nul
"C:\Program Files\7-Zip\7z.exe" a %cd%\Builds\%version%\osuPlayground-%version%-mac.zip %cd%\Builds\%version%\osuPlayground-%version%-mac\* > nul
"C:\Program Files\7-Zip\7z.exe" a %cd%\Builds\%version%\osuPlayground-%version%-linux.zip %cd%\Builds\%version%\osuPlayground-%version%-linux\* > nul

echo Done!