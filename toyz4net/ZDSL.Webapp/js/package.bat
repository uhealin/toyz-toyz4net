del Toyz4js.package.js
del Toyz4js.package.min.js
for /f %%i in (js-list.txt) do type %%i >> Toyz4js.package.js   
java -jar ../yuicompressor-2.4.6.jar --type js --charset utf-8  Toyz4js.package.js -o Toyz4js.package.min.js   

