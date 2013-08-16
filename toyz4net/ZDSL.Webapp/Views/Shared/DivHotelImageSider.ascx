<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="NHibernate" %>
<%@ Import Namespace="NHibernate.Criterion" %>

<%
    string basePath = WebUtil.GetWebRootPath();

    IList<HotelImageModel> hotelImages = ViewData[typeof(HotelImageModel).Name] as IList<HotelImageModel>;
    
    
 %>

<div id="divAdSider">
  <div class="ad" >
    <ul class="slider" >
     <%  int count = 0;
         foreach (HotelImageModel hotelImage in hotelImages)
        {
            if (!hotelImage.isNormal()||count>5) continue;
            count++;
            %>
      <li><img src="<%=hotelImage.imgUrl %>" title="<%=hotelImage.title %>" alt="<%=hotelImage.title %>" /></li>
     <%} %>
    </ul>
    
    <ul class="num" >
       <% for (int i = 0; i < count; i++)
          { %>
      <li><%=i+1 %></li>
      <%} %>
      
    </ul>
    
   
  </div>
</div>
<style  type="text/css">


#divAdSider .ad { 
	margin-bottom:10px;
	width:100%; 
	height:150px; 
	overflow:hidden;
	position:relative;
}
#divAdSider .slider,#divAdSider .num , #divAdSider .close{
	position:absolute;
}
#divAdSider .slider li{ 
	list-style:none;
	display:inline;
}
#divAdSider .slider img{ 

	display:block;
}

#divAdSider .close{ 
	right:5px; 
	top:5px;
	color: #069;
	text-align: center;
	line-height: 16px;
	width: 16px;
	height: 16px;
	font-family: Arial;
	font-size: 12px;
	cursor: pointer;
	overflow: hidden;
	margin: 3px 1px;
	border: 1px solid #069;
	background-color: #fff;
}

#divAdSider .close.on{ 
	color: #fff;
	line-height: 16px;
	width: 16px;
	height: 16px;
	font-size: 14px;
	margin: 3px 1px;
	border: 1px solid #069;
	background-color: #069;
	font-weight: bold;
}


#divAdSider .num{ 
	right:5px; 
	bottom:5px;
	
}
#divAdSider .num li{
	float: left;
	color: #069;
	text-align: center;
	line-height: 16px;
	width: 16px;
	height: 16px;
	font-family: Arial;
	font-size: 12px;
	cursor: pointer;
	overflow: hidden;
	margin: 3px 1px;
	border: 1px solid #069;
	background-color: #fff;
}
#divAdSider .num li.on{
	color: #fff;
	line-height: 16px;
	width: 16px;
	height: 16px;
	font-size: 14px;
	margin: 3px 1px;
	border: 1px solid #069;
	background-color: #069;
	font-weight: bold;
}

.slider
{
  width:100%;	
}

              .slider img
              {
                width:100%;
               height:150px;	
              }

</style>
 
	<script type="text/javascript">

	    /*首页广告效果*/
	    $(function() {
	        var len = $(".num > li").length;
	        var index = 0;
	        var adTimer;
	        $(".num li").mouseover(function() {
	            index = $(".num li").index(this);
	            showImg(index);
	        }).eq(0).mouseover();

	        //滑入 停止动画，滑出开始动画.
	        $('.ad').hover(function() {
	            clearInterval(adTimer);
	        }, function() {
	            adTimer = setInterval(function() {
	                showImg(index)
	                index++;
	                if (index == len) { index = 0; }
	            }, 2000);
	        }).trigger("mouseleave");

	        $(".close").mouseover(function() {
	            $(".close").addClass("on");
	        });

	        $(".close").mouseout(
	 	function() {
	 	    $(".close").removeClass("on");
	 	});

	        $(".close").click(function() {
	            $(".ad").hide();
	        });
	    })

	    // 通过控制top ，来显示不同的幻灯片
	    function showImg(index) {
	        var adHeight = $("#divAdSider .ad").height();
	        $(".slider").stop(true, false).animate({ bottom: -adHeight * index }, 1000);
	        $(".num li").removeClass("on")
			.eq(index).addClass("on");
	    }
	</script>	