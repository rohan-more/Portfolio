<html>
<head>

<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link href="patron.css" rel="stylesheet">
<title>Welcome to A.R.M.S.!</title>
</head>

<body>

<form action="LoginServlet" method="get">

<table class=layout cellspacing=0>
<tr>

<td class=sidebar>
<img src="logo_s.jpg" class="logo"><br><br>

<span class="loginBox">
Login:<br>
<input type="text" name="login">
</span>

<span class="loginBox">
Password:<br>
<input type="password" name="password">
</span>

<span class="loginBox">
<input type='submit' name='action' value='Submit'>

</span>

<br><br><br><br>
<span class=currentBarItem>Home</span>
<a href="accountsetup.jsp" class="sideBarItem">New Account</a>
<a href="MenuViewServlet" class="sideBarItem">View Menu</a>


</td>


<td class=content>
<h2 class="">
<span class='red'>
<% if (session.getAttribute("loginError")!= null){ %>
<%= (String)session.getAttribute("loginError") %>
<% } %>
</span>
</h2>

<br><br>
<h1 align="center">Welcome to A.R.M.S.!</h1><br><br>
<%@ include file="news.jsp" %>

</td>

</tr>
</table>

</form>
</body>
</html>
