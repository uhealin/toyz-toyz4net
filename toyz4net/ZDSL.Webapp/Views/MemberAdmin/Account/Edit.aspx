<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	中国商旅网个人信息管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
    MemberModel member = WebUtil.GetSessionAttr<MemberModel>(typeof(MemberModel).Name);
    string homeUrl = HomeController.GetPathIndex();
     %>


   <div class="left b" style=" width:70%">
   
   <div class="box">
                     <ul id="ulNav">
           
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="个人信息管理" href="<%= Request.Url.ToString() %>" >个人信息管理</a>  </li>
           </ul>
   </div>
   
     <h3>用户信息</h3>
     <div class="box">
         <form id="formMember" action="<%=basePath %>/MemberAdmin/Account/DoEdit" method="post">
        <input type="hidden" name="id" value="<%=member.id %>" />        
            <table >
                
                <tr>
                    
                    <td style="width:100px;text-align:right;">帐号：</td>
                    <td style="width:auto">
                        <%=member.id %>
                    </td>
                    <td><div class="Validform_checktip"></div></td>
                </tr>
                
                <tr>
                    
                    <td style="width:100px;text-align:right;">手机：</td>
                    <td style="width:auto">
                     <input type="text" value="<%=member.moblie %>" name="moblie" class="inputxt" datatype="m"  nullmsg="请输入手机号码！" errormsg="请输入有效手机号码！" />
                    </td>
                    <td><div class="Validform_checktip"></div></td>
                </tr>
                
                                <tr>
                    
                    <td style="width:100px;text-align:right;">邮箱：</td>
                    <td style="width:auto">
                     <input type="text" value="<%=member.email %>" name="email" class="inputxt" datatype="e"  nullmsg="请输入邮箱地址！" errormsg="请输入有效邮箱地址！" />
                    </td>
                    <td><div class="Validform_checktip"></div></td>
                </tr>
                
		        <tr>
                    
                    <td style="text-align:right;" >姓名：</td>
                    <td ><input type="text" value="<%=member.realName %>" name="realName" class="inputxt" datatype="*0-20"  nullmsg="请输入真实姓名！" /></td>
                    <td><div class="Validform_checktip">请输入真实姓名！</div></td>
                </tr>
                <tr>
                
                   
                   <td></td>
                   <td><button type="submit" >修改</button></td>
                   <td></td>
                </tr>
                </table>
           </form>
     </div>
     
     <h3>密码修改</h3>
     <div class="box">
         <form id="formResetPwd" action="<%=basePath %>/MemberAdmin/Account/DoResetPwd" method="post">
          <table>
            
                   <tr>
                    
                    <td style=" text-align:right; width:100px">当前密码：</td>
                    <td><input type="password"  name="pwd" class="inputxt" datatype="*6-16" nullmsg="请设置密码！" errormsg="密码范围在6~16位之间！" /></td>
                    <td><div class="Validform_checktip"></div></td>
                </tr>
                        
                 <tr>
                    
                    <td style=" text-align:right">新密码：</td>
                    <td><input type="password"  name="newPwd" class="inputxt" datatype="*6-16" nullmsg="请设置密码！" errormsg="密码范围在6~16位之间！" /></td>
                    <td><div class="Validform_checktip">密码范围在6~16个字符</div></td>
                </tr>
                <tr>
                    
                    <td style=" text-align:right" >确认密码：</td>
                    <td><input type="password" value="" name="pwd2" class="inputxt" datatype="*" recheck="newPwd" nullmsg="请再输入一次密码！" errormsg="您两次输入的账号密码不一致！" /></td>
                    <td><div class="Validform_checktip">两次输入密码需一致</div></td>
                </tr>
                           <tr>
            <td></td>
                   
                   <td><button type="submit" >修改</button></td>
                </tr>
          </table>
         
         </form>
      </div>
      
     <h3>新浪微博绑定</h3>
     <div class="box" style=" padding:5%">
    
               
                 <% if (string.IsNullOrEmpty(member.weiboUid))
                    { %>
                    未绑定
                    <a href="<%=basePath %>/MemberAdmin/Reg/DoWeiboAuth"><img src="<%=basePath %>/image/btn.weibo.login.png"  style=" height:28px; width:135px" /></a> 
                     
                 <%}
                    else
                    { %>
                     
                       <form  method="post" action="<%=basePath %>/MemberAdmin/Account/DoUnbind?t=weibo">
                        <a href="http://weibo.com/<%=member.weiboUid %>?s=6uyXnP" target="_blank"><img border="0" src="http://service.t.sina.com.cn/widget/qmd/<%=member.weiboUid %>/d1d89294/1.png"/></a>
                         <button type="submit">解除绑定</button>
                       </form>
                 <%} %>
              
     </div>
     
          <h3>人人网绑定</h3>
     <div class="box" style=" padding:5%">
    
               
                 <% if (string.IsNullOrEmpty(member.renrenUid))
                    { %>
                    未绑定
                    <a href="<%=basePath %>/MemberAdmin/Reg/DoRenrenAuth"><img src="<%=basePath %>/image/btn.renren.login.png"  style=" height:28px; width:135px" /></a> 
                     
                 <%}
                    else
                    {
                        //375736514
                        %>
                     
                       <form  method="post" action="<%=basePath %>/MemberAdmin/Account/DoUnbind?t=renren">
                        <a href="http://www.renren.com/<%=member.renrenUid %>?ss=17095&origin=92001" target="_blank"><img src="http://signature.xnimg.cn/badge/signature/sig/<%=member.renrenUid %>/7b3830edceaba1bbece149783c4475d3/0/3.png"></a>  
                         <button type="submit">解除绑定</button>
                       </form>
                 <%} %>
              
     </div>
     
          <h3>开心网绑定</h3>
     <div class="box" style=" padding:5%">
    
               
                 <% if (string.IsNullOrEmpty(member.kaixinUid))
                    { %>
                    未绑定
                    <a href="<%=basePath %>/MemberAdmin/Reg/DoKaixinAuth"><img src="<%=basePath %>/image/btn.kaixin.login.png"  style=" height:28px; width:135px" /></a> 
                     
                 <%}
                    else
                    {//41245720 %>
                     
                       <form  method="post" action="<%=basePath %>/MemberAdmin/Account/DoUnbind?t=kaixin">
                       
                        <img width="97" height="97" title="开心网UID： <%=member.kaixinUid %>" alt="开心网UID： <%=member.kaixinUid %>" src="http://pic.kaixin001.com.cn/logo/78/83/120_<%=member.kaixinUid %>_1.jpg">
                         <button type="submit">解除绑定</button>
                       </form>
                 <%} %>
              
     </div>
     
   </div>


    <div class="right b"  style=" width:29%">
       <h3>会员信息</h3>
       <div class="box">
          <% Html.RenderPartial("DivMemberInfo"); %>
       </div>
       <h3>快速搜索</h3>
       <div class="box">
       <% Html.RenderPartial("HotelQuickSearchBox"); %>
       </div>
       
    </div>



</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">

  <script type="text/javascript">

      $(document).ready(function() {



      $("#formMember,#formResetPwd").Validform({
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