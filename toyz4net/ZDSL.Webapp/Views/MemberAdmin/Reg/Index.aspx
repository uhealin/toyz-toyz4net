<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	中国商旅网会员注册
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <% 
       string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
       MemberModel member = ViewData[typeof(MemberModel).Name] as MemberModel;
       JsResultObject re = ViewData[typeof(JsResultObject).Name] as JsResultObject;
       string homeUrl = HomeController.GetPathIndex();
      
       %>
     <div class="box b" style=" margin-bottom:3px">
          
                                  
          
           <ul id="ulNav">
              <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="会员注册" href="<%=Request.Url.ToString() %>" >会员注册</a>  </li>
           </ul>
     </div>
 
     
    <div class="left b" style=" width:70%" >
    <%if (!string.IsNullOrEmpty(member.kaixinUid) || !string.IsNullOrEmpty(member.weiboUid) || !string.IsNullOrEmpty(member.renrenUid))
      { %>
    <h3>使用已注册用户进行绑定</h3>
     <div  style=" padding:15% ; font-size:1.2em;">
       <% Html.RenderPartial("FormLogin"); %>     
     </div>
     
     <%} %>
    
    <h3>新用户注册</h3>
    <div  style=" padding:15%;  font-size:1.2em;">
    

    <form id="formRegister" action="<%=basePath %>/Public/Reg/DoSubmit" method="post">
            <input type="hidden" name="weiboUid" value="<%=member.weiboUid %>" />
            <input type="hidden" name="renrenUid" value="<%=member.renrenUid %>" />
            <input type="hidden" name="kaixinUid" value="<%=member.kaixinUid %>" />
        
            <table cellpadding="5" >
            
                <tr>
                    
                    <td style="width:100px; text-align:right;">用户名：</td>
                    <td style="width:auto">
                    <input type="text" value="<%=member.id %>" name="id" class="inputxt" datatype="n0-12"  nullmsg="请输入您的用户名！" errormsg="由英文字母或数字组成（不超过12）" />


                    </td>
                    <td><div class="Validform_checktip">请输入12个字符以下的用户名</div></td>
                </tr>
            
            
                <tr>
                    
                    <td style=" text-align:right">密码：</td>
                    <td><input type="password" value="<%=member.pwd %>" name="pwd" class="inputxt" datatype="*6-16" nullmsg="请设置密码！" errormsg="密码范围在6~16位之间！" /></td>
                    <td><div class="Validform_checktip">密码范围在6~16个字符</div></td>
                </tr>
                <tr>
                    
                    <td style=" text-align:right" >确认密码：</td>
                    <td><input type="password" value="" name="pwd2" class="inputxt" datatype="*" recheck="pwd" nullmsg="请再输入一次密码！" errormsg="您两次输入的账号密码不一致！" /></td>
                    <td><div class="Validform_checktip">两次输入密码需一致</div></td>
                </tr>
                
               <tr>
                    
                    <td  style=" text-align:right" >姓名：</td>
                    <td ><input type="text" value="<%=member.realName %>" name="realName" class="inputxt" datatype="*0-30"  nullmsg="请输入姓名！" errormsg="请输入6~30个字符！" /></td>
                    <td><div class="Validform_checktip">请填写真实姓名，以便发放点评奖金</div></td>
                </tr>
            

            


               <tr>
                    
                    <td  style=" text-align:right" >手机：</td>
                    <td ><input type="text" value="<%=member.moblie %>" name="moblie" class="inputxt" datatype="m"  nullmsg="请输入手机号码！" errormsg="请输入有效的手机号码！" /></td>
                    <td><div class="Validform_checktip">请正确填写您的手机号码，以便接收预订确认短信。</div></td>
                </tr>
                
                <tr>
                    
                    <td style="width:100px; text-align:right;">邮箱帐号：</td>
                    <td style="width:auto">
                    <input type="text" value="<%=member.id %>" name="email" class="inputxt" datatype="e"  nullmsg="请输入您常用的邮箱！" errormsg="请输入您常用的邮箱！" />


                    </td>
                    <td><div class="Validform_checktip">请正确填写您的邮址，以便接收确认通知或服务问询。</div></td>
                </tr>
 
                <tr>
                   
                   <td></td>
                   <td><button type="submit" >提交</button> <button type="reset" >重置</button></td>
                </tr>
	        </table>
       
    
        
        

     
           
         </form>
         </div>
    </div>
    
    
        <div class="right b" style=" width:29%" >
        
        
         <h3>快速查询</h3>
         <div id="divQuickSearch" class="box">
           <% Html.RenderPartial("HotelQuickSearchBox"); %>
         </div>
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

  <script type="text/javascript">

      $(document).ready(function() {

          $("#divRadioTypes").buttonset();
          $("#divRadioSexs").buttonset();

          $("#formRegister").Validform({
              tiptype: function(msg, o, cssctl) {
                  //msg：提示信息;
                  //o:{obj:*,type:*}, obj指向的是当前验证的表单元素（或表单对象），type指示提示的状态，值为1、2、3、4， 1：正在检测/提交数据，2：通过验证，3：验证失败，4：提示ignore状态;
                  //cssctl:内置的提示信息样式控制函数，该函数需传入两个参数：显示提示信息的对象 和 当前提示的状态（既形参o中的type）;
                  if (!o.obj.is("form")) {//验证表单元素时o.obj为该表单元素，全部验证通过提交表单时o.obj为该表单对象;
                      var objtip = o.obj.parent().next().find(".Validform_checktip");
                      cssctl(objtip, o.type);
                      objtip.text(msg);
                  } else {
                      var objtip = o.obj.find("#msgdemo");
                      cssctl(objtip, o.type);
                      objtip.text(msg);
                  }
              },
              datatype: {//传入自定义datatype类型【方式二】;
                  "z2-4": /^[\u4E00-\u9FA5\uf900-\ufa2d]{2,4}$/,
                  "*6-20": /^[^\s]{6,20}$/,
                  "n0-12": /^[a-z,0-9]{0,12}$/

              },
              // ajaxPost: true,
              postonce: true
          });

          SetHotelConditionForm("#divQuickSearch");

      });      //$(document).ready
    
  </script>
</asp:Content>
