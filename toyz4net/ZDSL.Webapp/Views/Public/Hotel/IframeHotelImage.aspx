<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
         <% 
             string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
             IList<HotelImageModel> hotelImages = ViewData[typeof(HotelImageModel).Name] as IList<HotelImageModel>;
              %>
        <link rel="stylesheet" href="<%= basePath  %>/js3rd/jquery-galleriffic-2.0/css/basic.css" type="text/css" />
		<link rel="stylesheet" href="<%= basePath  %>/js3rd/jquery-galleriffic-2.0/css/galleriffic-2.css" type="text/css" />		
        
        
        <style>
         
         div.slideshow a.advance-link {
               display : block;
                 height :auto;
                line-height:normal;
                text-align:0px;
                width:auto;
                 padding:5%;
}

div.slideshow-container {
    clear: both;
    height: 250px;
    position: relative;
    overflow:visible;
}

span.image-caption {
    display:inline;
     position:relative;
    text-align:left;
    width:  auto;
}
        
        </style>
        
        
        <script language="javascript" type="text/javascript" src="<%= basePath  %>/js3rd/jquery/jquery-1.6.2.min.js" ></script>
        <script type="text/javascript" src="<%= basePath  %>/js3rd/jquery-galleriffic-2.0/js/jquery.galleriffic.js"></script>
		<script type="text/javascript" src="<%= basePath  %>/js3rd/jquery-galleriffic-2.0/js/jquery.opacityrollover.js"></script>
     
     
        <script type="text/javascript">

            $(document).ready(function() {

            $('div.content').css('display', 'block');

            // Initially set opacity on thumbs and add
            // additional styling for hover effect on thumbs
            var onMouseOutOpacity = 0.67;
            $('#thumbs ul.thumbs li').opacityrollover({
                mouseOutOpacity: onMouseOutOpacity,
                mouseOverOpacity: 1.0,
                fadeSpeed: 'fast',
                exemptionSelector: '.selected'
            });

            // Initialize Advanced Galleriffic Gallery
            var gallery = $('#thumbs').galleriffic({
                delay: 2500,
                numThumbs: 10,
                preloadAhead: 10,
                enableTopPager: true,
                enableBottomPager: true,
                maxPagesToShow: 7,
                imageContainerSel: '#slideshow',
                controlsContainerSel: '#controls',
                captionContainerSel: '#caption',
                loadingContainerSel: '#loading',
                renderSSControls: true,
                renderNavControls: true,
                playLinkText: 'Play Slideshow',
                pauseLinkText: 'Pause Slideshow',
                prevLinkText: '&lsaquo; Previous Photo',
                nextLinkText: 'Next Photo &rsaquo;',
                nextPageLinkText: 'Next &rsaquo;',
                prevPageLinkText: '&lsaquo; Prev',
                enableHistory: false,
                autoStart: false,
                syncTransitions: true,
                defaultTransitionDuration: 900,
                onSlideChange: function(prevIndex, nextIndex) {
                    // 'this' refers to the gallery, which is an extension of $('#thumbs')
                    this.find('ul.thumbs').children()
							.eq(prevIndex).fadeTo('fast', onMouseOutOpacity).end()
							.eq(nextIndex).fadeTo('fast', 1.0);
                },
                onPageTransitionOut: function(callback) {
                    this.fadeTo('fast', 0.0, callback);
                },
                onPageTransitionIn: function() {
                    this.fadeTo('fast', 1.0);
                }
            });
       });
        
        </script>
</head>
<body style=" width:98%; height:98%">
    


				<div id="thumbs" class="navigation" style="  float:left; width:39%">
					<ul class="thumbs noscript">
					<% foreach(HotelImageModel hotelImage in hotelImages) {
           // if (hotelImage.yardage != HotelImageModel.YEADAGE_LARGE) continue;
                     %>
						<li>
							<a class="thumb" name="leaf" href="<%=hotelImage.imgUrl %>" title="Title #0">
								<img src="<%=hotelImage.imgUrl %>" alt="Title #0" style=" max-width:100px" />
							</a>
							<div class="caption">
								<div class="image-title"><%= hotelImage.title %></div>
								<div class="image-desc">Description</div>
							</div>
						</li>
                      <%} %>
						
					</ul>
				</div>
		    				<!-- Start Advanced Gallery Html Containers -->
				<div id="gallery" class="content" style=" float:left; width:60%">
					
					<div class="slideshow-container">
						<div id="loading" class="loader"></div>
						<div id="slideshow" class="slideshow"></div>
					</div>
					<div id="caption" class="caption-container"></div>
				</div>
   


</body>
</html>
