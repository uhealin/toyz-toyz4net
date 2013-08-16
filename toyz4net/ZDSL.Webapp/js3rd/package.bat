del js3rd.package.js
del js3rd.package.min.js
for /f %%i in (js-list.txt) do type %%i >> js3rd.package.js   
java -jar ../yuicompressor-2.4.6.jar --type js --charset utf-8  js3rd.package.js -o js3rd.package.min.js   

