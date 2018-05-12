@echo off
set /p version="Version tag (v1.x.x): "

echo Building...

"C:\Program Files\Unity\Editor\Unity" -batchmode -quit -projectPath %cd% -executeMethod ApplicationBuilder.Build -version %version% -buildfolder %cd%\Builds
echo Copying scripts...

robocopy /s /mir %cd%\Scripts\ %cd%\Builds\%version%\osuPlayground-%version%-win32\Scripts\
robocopy /s /mir %cd%\Scripts\ %cd%\Builds\%version%\osuPlayground-%version%-win64\Scripts\
robocopy /s /mir %cd%\Scripts\ %cd%\Builds\%version%\osuPlayground-%version%-mac\Scripts\

echo Zipping...

"C:\Program Files\7-Zip\7z.exe" a %cd%\Builds\%version%\osuPlayground-%version%-win32.zip %cd%\Builds\%version%\osuPlayground-%version%-win32\*
"C:\Program Files\7-Zip\7z.exe" a %cd%\Builds\%version%\osuPlayground-%version%-win64.zip %cd%\Builds\%version%\osuPlayground-%version%-win64\*
"C:\Program Files\7-Zip\7z.exe" a %cd%\Builds\%version%\osuPlayground-%version%-mac.zip %cd%\Builds\%version%\osuPlayground-%version%-mac\*

echo Done!