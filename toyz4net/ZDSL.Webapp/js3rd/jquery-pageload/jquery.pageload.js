/** 
 *  页面加载等待页面 
 * 
 * @author gxjiang 
 * @date 2010/7/24 
 * 
 */  
 //var height = window.screen.height-250;  
 //var width = window.screen.width;  
 
  //var _wiw=window.innerWidth;
 // var _wih=window.innerHeight;

// var leftW = 300;  
// if(width>1200){  
//    leftW = 500;  
// }else if(width>1000){  
 //   leftW = 350;  
 //}else {  
//    leftW = 100;  
 //}  
 
 // var _pageloadHeight=40;
 // var _pageloadWidth=200;

  
  
   
 var _html = "<div id='loading' style='position:absolute;left:0;width:100%;height:100%;top:0;background:#E0ECFF;opacity:0.8;filter:alpha(opacity=80);text-align:center;padding-top:10% '>"  
 +"<div style=' cursor1:wait;width:200px;height:50px;text-align:center;margin:0 auto;border:2px solid #ccc;color:#000;background:white;'>正在加载，请等待...</div></div>";  
 
  document.write(_html);
 
   
 $(document).ready(function(){  
    $("#loading").remove();  
 });
  
       
