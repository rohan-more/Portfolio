<html>
<head>
<link href="patron.css" rel=stylesheet type="text/css">
<title>A.R.M.S. - Account Edit</title>
</head>

<body>
<% 
if (session.getAttribute("id")== null || session.getAttribute("id").equals(""))
    response.sendRedirect("index.jsp");
%>
<jsp:useBean id="account" scope="session" class="cupojava.AccountInfoBean" />
<form action='/arms/AccountEditServlet' method="get">
<table class=layout cellspacing=0>
<tr>

<td class=sidebar>
<img src="logo_s.jpg" class="logo"><br><br>
<a href="action.jsp" class=sideBarItem>Home</a><br>
<a href="MenuServlet" class="sideBarItem">Order</a><br>
<span class=currentBarItem>My Account</span><br>
<a href="SignOutServlet" class="sideBarItem">Sign Out</a><br>
</td>


<td class=content>

<!-- This next line is the main part of this page. It ouputs the menu -->
<table width="100%"  border="0">
  <tr>
    <td width="13%"><%=account.flagFirstName()%>First Name: </td>
    <td width="20%"><input type="text" name="firstName" value="<jsp:getProperty name='account' property='firstName' />"></td>
    <td width="13%">Middle Name:</td>
    <td width="20%"><input type="text" name="middleName" value="<jsp:getProperty name='account' property='middleName' />"></td>
    <td width="13%"><%=account.flagLastName()%>Last Name:</td>
    <td width="21%"><input type="text" name="lastName" value="<jsp:getProperty name='account' property='lastName' />"></td>
  </tr>
</table>
<table width="100%"  border="0">
  <tr>
    <td width="13%"><%=account.flagLogin()%>ARMS ID:</td>
    <td width="87%"><jsp:getProperty name='account' property='login' /></td>
  </tr>
  <tr>
    <td><%=account.flagPassword()%>Password:</td>
    <td><input type="password" name="password" value="<jsp:getProperty name='account' property='password' />"></td>
  </tr>
  <tr>
    <td><%=account.flagPassword()%>Re-Type Password:</td>
    <td><input type="password" name="password2" value="<jsp:getProperty name='account' property='password2' />"></td>
  </tr>
  <tr>
    <td><%=account.flagAge()%>Age:</td>
    <td><input size="2" maxlength="2" name="age" value="<jsp:getProperty name='account' property='age' />">
  </tr>
</table>
<table width="100%"  border="0">
  <tr>
    <td width="13%" height="24"><%=account.flagEmail()%>Email: </td>
    <td width="53%"><input type="text" name="email" value="<jsp:getProperty name='account' property='email' />"></td>
    <td width="13%"><%=account.flagPhone()%>Phone #: </td>
    <td width="21%"><input type="text" name="phone" value="<jsp:getProperty name='account' property='phone' />"></td>
  </tr>
</table>
<table width="100%"  border="0">
  <tr>
    <td width="13%"><%=account.flagAddress1()%>Address 1: </td>
    <td width="20%"><input type="text" name="address1" value="<jsp:getProperty name='account' property='address1' />"></td>
    <td width="13%">&nbsp;</td>
    <td width="20%">&nbsp;</td>
    <td width="13%">&nbsp;</td>
    <td width="21%">&nbsp;</td>
  </tr>
  <tr>
    <td>Address 2: </td>
    <td><input type="text" name="address2" value="<jsp:getProperty name='account' property='address2' />"></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><%=account.flagCity()%>City:</td>
    <td><input type="text" name="city" value="<jsp:getProperty name='account' property='city' />"></td>
    <td><%=account.flagState()%>State:</td>
    <td><input type="text" size="2" maxlength="2" name="state" value="<jsp:getProperty name='account' property='state' />"></td>
    <td><%=account.flagZip()%>Zip:</td>
    <td><input type="text" size="5" maxlength="5" name="zip" value="<jsp:getProperty name='account' property='zip' />"></td>
  </tr>
</table>
<p>&nbsp;</p>
<input type='submit' name='action' value='Submit'>
</td></tr></table>
</td>

</tr>
</table>
</form>
</body>
</html>

