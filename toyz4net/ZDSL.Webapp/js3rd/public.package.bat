del public.js3rd.package.js
del public.js3rd.package.min.js
for /f %%i in (public.js-list.txt) do type %%i >> public.js3rd.package.js   
java -jar ../yuicompressor-2.4.6.jar --type js --charset utf-8  public.js3rd.package.js -o public.js3rd.package.min.js   

