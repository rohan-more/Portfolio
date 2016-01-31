<html>
<head>
<link href="intranet.css" rel=stylesheet type="text/css">
<title>A.R.M.S. - Edit Account</title>
</head> 

<body>
<% 
if (session.getAttribute("id")== null || session.getAttribute("id").equals(""))
    response.sendRedirect("index.jsp");
%>
<jsp:useBean id="account2" scope="session" class="intracupojava.AccountInfoBean" />
<form action='/intraarms/AccountEditServlet2' method="get">
<table class=layout cellspacing=0>
<tr>

<td class="sidebar">
<img src="logo_s.jpg" class="logo"><br><br>
<a href="action.jsp" class="sideBarItem">Home</a>
<a href="MenuViewServlet" class="sideBarItem">View Menu</a>
<a href="SetRedirectServlet" class=sideBarItem>My Account</a>
<% 
String temp= (String)session.getAttribute("permission");
if (temp!= null){
    temp= temp.toLowerCase();}
if (temp.equals("m") || temp.equals("e")
    || temp.equals("s") || temp.equals("a")){ 
if (temp.equals("m") 
    || temp.equals("s") || temp.equals("a")){ %>
<span class="currentBarItem">Account Management</span><br />
<%}%>
<a href="MenuManagementServlet" class="sideBarItem">Menu Management</a>
<%} else response.sendRedirect("/arms");%>
<a href="SignOutServlet" class="sideBarItem">Sign Out</a><br>
</td>


<td class=content>

<!-- This next line is the main part of this page. It ouputs the menu -->
<table width="100%"  border="0">
  <tr>
    <td width="13%"><%=account2.flagFirstName()%>First Name: </td>
    <td width="20%"><input type="text" name="firstName" value="<jsp:getProperty name='account2' property='firstName' />"></td>
    <td width="13%">Middle Name:</td>
    <td width="20%"><input type="text" name="middleName" value="<jsp:getProperty name='account2' property='middleName' />"></td>
    <td width="13%"><%=account2.flagLastName()%>Last Name:</td>
    <td width="21%"><input type="text" name="lastName" value="<jsp:getProperty name='account2' property='lastName' />"></td>
  </tr>
</table>
<table width="100%"  border="0">
  <tr>
    <td width="13%"><%=account2.flagLogin()%>ARMS ID:</td>
    <td width="20%"><jsp:getProperty name='account2' property='login' /></td>
    <td width="33%">&nbsp;</td>
    <td width="13%"><%=account2.flagType()%>Type:</td>
    <td width="20%"><jsp:getProperty name='account2' property='type' /></td>
  </tr>
  <tr>
    <td><%=account2.flagPassword()%>Password:</td>
    <td><input type="password" name="password" value="<jsp:getProperty name='account2' property='password' />"></td>
  </tr>
  <tr>
    <td><%=account2.flagPassword()%>Re-Type Password:</td>
    <td><input type="password" name="password2" value="<jsp:getProperty name='account2' property='password2' />"></td>
  </tr>
  <tr>
    <td><%=account2.flagAge()%>Age:</td>
    <td><input size="2" maxlength="2" name="age" value="<jsp:getProperty name='account2' property='age' />">
  </tr>
</table>
<table width="100%"  border="0">
  <tr>
    <td width="13%" height="24"><%=account2.flagEmail()%>Email: </td>
    <td width="53%"><input type="text" name="email" value="<jsp:getProperty name='account2' property='email' />"></td>
    <td width="13%"><%=account2.flagPhone()%>Phone #: </td>
    <td width="21%"><input type="text" name="phone" value="<jsp:getProperty name='account2' property='phone' />"></td>
  </tr>
</table>
<table width="100%"  border="0">
  <tr>
    <td width="13%"><%=account2.flagAddress1()%>Address 1: </td>
    <td width="20%"><input type="text" name="address1" value="<jsp:getProperty name='account2' property='address1' />"></td>
    <td width="13%">&nbsp;</td>
    <td width="20%">&nbsp;</td>
    <td width="13%">&nbsp;</td>
    <td width="21%">&nbsp;</td>
  </tr>
  <tr>
    <td>Address 2: </td>
    <td><input type="text" name="address2" value="<jsp:getProperty name='account2' property='address2' />"></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><%=account2.flagCity()%>City:</td>
    <td><input type="text" name="city" value="<jsp:getProperty name='account2' property='city' />"></td>
    <td><%=account2.flagState()%>State:</td>
    <td><input type="text" size="2" maxlength="2" name="state" value="<jsp:getProperty name='account2' property='state' />"></td>
    <td><%=account2.flagZip()%>Zip:</td>
    <td><input type="text" size="5" maxlength="5" name="zip" value="<jsp:getProperty name='account2' property='zip' />"></td>
  </tr>
</table>
<p>&nbsp;</p>
<table width="100%">
<tr>
<td align="left"><input type='submit' name='action' value='   Edit   '></td>
<td align="right"><input type='submit' name='action' value='Delete' onclick="alert('Are you sure you want to delete the account!?'); return true"></td>
</tr>
</table>

</td></tr></table>
</td>

</tr>
</table>
</form>
</body>
</html>


