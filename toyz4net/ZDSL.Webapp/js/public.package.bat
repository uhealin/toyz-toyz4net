del public.package.js
del public.package.min.js
for /f %%i in (public.js-list.txt) do type %%i >> public.package.js   
java -jar ../yuicompressor-2.4.6.jar --type js --charset utf-8  public.package.js -o public.package.min.js   

