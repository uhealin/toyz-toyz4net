<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>

<%
  
     string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();

    
 %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>TestWeibo</title>
    
    <script type="text/javascript">


        function getarg() {
            var url = document.location.href;
            var arg=url.split("#");
            return arg[1];
        }


        document.location.href = "<%=basePath %>/MemberAdmin/Reg/QQAuthResponse?" + getarg();
         
      

    </script>
</head>
<body>
    <div>
 
    </div>
</body>
</html>
