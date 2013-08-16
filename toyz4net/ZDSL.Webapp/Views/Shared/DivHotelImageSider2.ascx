<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="NHibernate" %>
<%@ Import Namespace="NHibernate.Criterion" %>

<%
    string basePath = WebUtil.GetWebRootPath();

    IList<HotelImageModel> hotelImages = ViewData[typeof(HotelImageModel).Name] as IList<HotelImageModel>;
    HotelImageModel mainImg = new HotelImageModel();
    foreach (HotelImageModel hotelImage in hotelImages)
    {
        if (hotelImage.isNormal()) {
            mainImg = hotelImage;
            break;
        }
    }
    
 %>

<div>
<img class="imgMain" src="<%=mainImg.imgUrl %>" title="<%=mainImg.title %>" alt="<%=mainImg.title %>" onclick="$('#tabs').tabs('select',1);" />
<div  style=" overflow-x:hidden">
<table width="1002" border="0" align="center" cellpadding="0" cellspacing="0">
<tr>
    <td  valign="top" background="images/pro_centerbg2.jpg"><table width="1002"  border="0" cellpadding="0" cellspacing="0">
      <tr>
        
        <td width="790" valign="top">
   <!----------------------------小圖展示區------------------------------------>
   <div id=demo style="overflow:hidden;width:790px;">
   <table>
    <tr>
     <td id="demo1" valign="top">
      <table  border="0" cellpadding="2" cellspacing="2">
       <tr>
            <%  
                foreach (HotelImageModel hotelImage in hotelImages)
                {
                    if (!hotelImage.isNormal()) continue;
            
            %>
       <td  align="center" valign="top" >
        <DIV class="example" >
        <!--[if lte IE 6.5]><v:rect filled="f"><v:fill size="0pt,0pt"></v:fill><![endif]-->
        <DIV class="bd" >
         <img src="<%=hotelImage.imgUrl %>" title="<%=hotelImage.title %>" alt="<%=hotelImage.title %>" onclick="$('.imgMain').attr('src',this.src).attr('title',this.title).attr('alt',this.alt);" />
         </DIV>
        <!--[if lte IE 6.5]></v:rect><![endif]-->
        </DIV>
       </td>
       <%} %>
       </tr>
      </table>
     </td>
     <td valign="top" id="demo2"></td>
    </tr>
   </table>
   </div>
  
   <!----------------------------小圖展示區end------------------------------------>
   </td>
     </tr>
    </table></td>
</tr>
</table>
</div>
 
</div>
<style type="text/css">

  #demo img
  {
    width:60px;
    height:40px;
  	}
 
 .imgMain
 {
 	 width:100%;
 	 height:120px;
 	}
</style>

<script type="text/javascript">
    var speed = 30;
    var addspeed = 1;
    var dafault_addspeed = 1;
    var demo = document.getElementById("demo");
    var demo1 = document.getElementById("demo1");
    var demo2 = document.getElementById("demo2");
    demo2.innerHTML = demo1.innerHTML
    function Marquee() {
        if (demo2.offsetWidth - demo.scrollLeft <= 0)
            demo.scrollLeft -= demo1.offsetWidth
        else {
            demo.scrollLeft = demo.scrollLeft + addspeed
        }
    }
    function rightAddSpeed() {
        if (demo2.offsetWidth - demo.scrollLeft > 0)
            demo.scrollLeft += demo1.offsetWidth
        else {
            demo.scrollLeft = demo.scrollLeft - 10
        }
    }
    var MyMar = setInterval(Marquee, speed)
    demo.onmouseover = function() { clearInterval(MyMar) }
    demo.onmouseout = function() { MyMar = setInterval(Marquee, speed) } 
</script>