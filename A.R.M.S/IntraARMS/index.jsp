<html>
<head>

<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link href="intranet.css" rel="stylesheet">
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

</td>


<td class=content>
<h2 class="">
<span class='red'>
<% if (session.getAttribute("loginError")!= null){ %>
<%= (String)session.getAttribute("loginError") %>
<% session.setAttribute("loginError", "");} %>
</span>
</h2>

<h3>Welcome to A.R.M.S.!</h3>



</td>

</tr>
</table>

</form>
</body>
</html>
