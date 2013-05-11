<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pay.aspx.cs" Inherits="RenRenAssistant.Pay" %>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>弹弹堂 - 点劵充值兑换</title>
<script language=JavaScript>
function logout(){
	if (confirm("您确定要兑换校内豆吗？"))
	top.location = "../123.aspx";
	return false;
}
function select_onclick() {
    var value = parseInt(document.getElementById("select").value)/100;
    document.getElementById("AccountCount").innerHTML = value;
}
function Charge_Submit()
{

    var realAccout = parseInt(document.getElementById("tdAccount").innerHTML);
    var accout = parseInt(document.getElementById("AccountCount").innerHTML);
    if(accout > realAccout)
    {
        document.getElementById("pay_no").style.display = "";
    }
    else
    {
        document.getElementById("btnSubmit").click();
    }    
}
function Show_Channel() {
    var value = document.getElementById("txtChannel").value;
	if(value=='s1')
	{
	    document.getElementById("tdChannel").innerHTML='弹弹一区';
	}
	else
	{
	   document.getElementById("tdChannel").innerHTML='弹弹二区';
	}
}
</script>

<style type="text/css">
<!--
body {
	margin: 0px;
	padding:0px;
	text-align:left;
	font-size:16px;
	font-family:Verdana, Arial, Helvetica, sans-serif;
	background-image:url(images/bg.jpg);
	background-repeat:repeat-x;
	background-color:#F9F2BC;
	font-weight:bold;
}
.pay_main{
	font-family:Verdana, Arial, Helvetica, sans-serif;
	font-size:16px;
	font-weight:bold;
}
select{
	font-family:Verdana, Arial, Helvetica, sans-serif;
	font-size:16px;
	background-color:#F2E9BD;
	color:#512E0B;
	border:#512E0B solid 1px;
}
.pay_no{
	color:#FF0000;
	font-size:14px;
	font-family:Verdana, Arial, Helvetica, sans-serif;
	font-weight:bold;
}
-->
</style></head>

<body onload='Show_Channel()'>
    <form id="form1" runat="server">
    
<table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0">
  <tr style="display:none" >
    <td><table><tr><td><asp:Button ID="btnSubmit" Text="btnSubmit" runat ="server" 
        onclick="btnSubmit_Click1" /></td></tr>
        <tr><td><asp:TextBox ID="txtChannel" Text="s1"  runat ="server" />
        </td></tr></table></td></tr>
  <tr>
    <td><table width="775" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td height="456" valign="top" background="images/bg2.gif">
        <div class="pay_main">
        <table width="775" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td height="156">
              </td>
          </tr>
          <tr>
            <td valign="top"><table width="775" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="300" height="40">&nbsp;</td>
                <td id="tdAccount" runat="server" >0</td>
              </tr>
              <tr>
                <td height="40">&nbsp;</td>
                <td><label>
                  <select runat="server" name="select" id="select" onchange="return select_onclick()" >
                    <option value="100">100</option>
                    <option value="500">500</option>
                    <option value="1000">1000</option>
                    <option value="1500">1500</option>
                    <option value="2000">2000</option>
                    <option value="3000">3000</option>
                    <option value="5000">5000</option>
                    <option value="10000">10000</option>
                    <option value="20000">20000</option>
                    <option value="50000">50000</option>
                  </select>
                点劵，需要花费<div id="AccountCount" style="display:inline">1</div>校内豆。</label></td>
              </tr>
              <tr>
                <td height="40">&nbsp;</td>
                <td id="tdChannel" >弹弹一区</td>
              </tr>
              <tr>
                <td height="50">&nbsp;</td>
                <td><table width="268" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="132" height="40" runat="server"><a href="#" target="_self" onClick="Charge_Submit();"><img src="images/dj_icon.jpg" width="124" height="40" border="0"></a></td>
    <td width="132"><a href="http://pay.renren.com"><img src="images/pay_icon.jpg" width="124" height="40" border="0"></a></td>
  </tr>
</table></td>
              </tr>
              <tr>
                <td height="36">&nbsp;</td>
                <td><div class="pay_no" id="pay_no" style="display:none;">校内豆不足，请充值校内豆！</div></td>
                <td><div class="pay_no" id="pay_error" style="display:none;">充值失败，请重新尝试！</div></td>
              </tr>
            </table>
              </td>
          </tr>
        </table>
        </div>
        </td>
      </tr>
    </table></td>
  </tr>
</table>
    </form>
</body>
</html>
