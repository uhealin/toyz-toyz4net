<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Cache" %>
<%@ Import Namespace="ZDSL.Model.Public" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<%@ Import Namespace="ZDSL.Biz" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PageSeoConfig
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

      <% string basePath = WebUtil.GetWebRootPath();
         PageSeoModel seo = ViewData[typeof(PageSeoModel).Name] as PageSeoModel;
        %>
    <div region="center">
       <form id="form1" method="post">
       <input type="hidden" name="id" value="<%=seo.id %>" />
        
        <fieldset>
          <legend>首页</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2" name="homeIndexTitle"><%=seo.homeIndexTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2" name="homeIndexKeywords"><%=seo.homeIndexKeywords %></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3"  name="homeIndexDesc" ><%=seo.homeIndexDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        
         <fieldset>
          <legend>酒店预定页</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="hotelIndexTitle" ><%=seo.hotelIndexTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="hotelIndexKeywords" ><%=seo.hotelIndexKeywords %></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="hotelIndexDesc" ><%=seo.hotelIndexDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        
        <fieldset>
          <legend>酒店单页( {0}:酒店id,{1}:酒店名,{2}:酒店地址, {3}:酒店描述, {4}:城市code,{5}:城市名 )</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="hotelDetailTitle" ><%=seo.hotelDetailTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="hotelDetailKeywords" ><%=seo.hotelDetailKeywords %></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="hotelDetailDesc" ><%=seo.hotelDetailDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        
        <fieldset>
          <legend>酒店搜索页面( {0}:城市名,{1}:酒店名,{2}:入住日期 )</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="hotelSearchTitle" ><%=seo.hotelSearchTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="hotelSearchKeywords" ><%=seo.hotelSearchKeywords%></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="hotelSearchDesc" ><%=seo.hotelSearchDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        
        <fieldset>
          <legend>城市页面( {0}城市code,{1}:城市名 )</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="cityTitle" ><%=seo.cityTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="cityKeywords" ><%=seo.cityKeywords%></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="cityDesc" ><%=seo.cityDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        
       <fieldset>
          <legend>城市商业区页面 ( {0}城市code,{1}:城市名,{2}:区id, {3}:区名 )</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="cityClTitle" ><%=seo.cityClTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="cityClKeywords" ><%=seo.cityClKeywords%></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="cityClDesc" ><%=seo.cityClDesc%></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        
                <fieldset>
          <legend>城市行政区页面 ( {0}城市code,{1}:城市名,{2}:区id, {3}:区名 )</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="cityDTitle" ><%=seo.cityDTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="cityDKeywords" ><%=seo.cityDKeywords%></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="cityDDesc" ><%=seo.cityDDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        
          <fieldset>
          <legend>连锁品牌首页面</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="brandIndexTitle" ><%=seo.brandIndexTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="brandIndexKeywords" ><%=seo.brandIndexKeywords%></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="brandIndexDesc" ><%=seo.brandIndexDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        <fieldset>
          <legend>连锁品牌单页面 ( {0}品牌code,{1}:品牌长名,{2}:品牌短名, {3}:拼音 )</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="brandHotelTitle" ><%=seo.brandHotelTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="brandHotelKeywords" ><%=seo.brandHotelKeywords%></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="brandHotelDesc" ><%=seo.brandHotelDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        
         <fieldset>
          <legend>新闻首页面</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="newsIndexTitle" ><%=seo.newsIndexTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="newsIndexKeywords" ><%=seo.newsIndexKeywords%></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="newsIndexDesc" ><%=seo.newsIndexDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        <fieldset>
          <legend>新闻单页面 ( {0}新闻code,{1}:新闻主题,{2}:新闻日期 ,{3}:新闻内容 )</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="newsHotelTitle" ><%=seo.newsHotelTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="newsHotelKeywords" ><%=seo.newsHotelKeywords%></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="newsHotelDesc" ><%=seo.newsHotelDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        
                  <fieldset>
          <legend>展会首页面</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="exhiIndexTitle" ><%=seo.exhiIndexTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="exhiIndexKeywords" ><%=seo.exhiIndexKeywords%></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="exhiIndexDesc" ><%=seo.exhiIndexDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        <fieldset>
          <legend>展会单页面 ( {0}展会code,{1}:展会名,{2}:展会地址, {3}:展会时间 ,{4}:行业,{5}:内容 )</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="exhiHotelTitle" ><%=seo.exhiHotelTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="exhiHotelKeywords" ><%=seo.exhiHotelKeywords%></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="exhiHotelDesc" ><%=seo.exhiHotelDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
        
                <fieldset>
          <legend>点评首页</legend>
         <table class="tableSimple" style=" width:100%">
          <tr>
            <th  >title：</th>
            <td><textarea cols="200" rows="2"  name="commentIndexTitle" ><%=seo.commentIndexTitle %></textarea></td>
          </tr>
           <tr>
            <th >keyword：</th>
            <td><textarea cols="200" rows="2"  name="commentIndexKeywords" ><%=seo.commentIndexKeywords%></textarea></td>
          </tr>
          <tr>
            <th >desc：</th>
            <td><textarea cols="200" rows="3" name="commentIndexDesc" ><%=seo.commentIndexDesc %></textarea></td>
          </tr>

        </table>
        </fieldset>
        
       
<a class="easyui-linkbutton" iconCls="icon-edit" onclick="doSubmit()" >保存</a>
    </form>
 </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">


      <%
    
       string basePath = WebUtil.GetWebRootPath();
            
    %>
   
    <script type="text/javascript">


        function doSubmit() {

            $("#form1").form("submit", {

                url: "<%=basePath %>/Admin/Page/DoSavePageSeo"

            });

        }

        $(document).ready(function() {

            var indOpt = GetIndEditor()["options"];

            
        });
    
    </script>

</asp:Content>
