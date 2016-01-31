<html>
<head>
<link href="patron.css" rel=stylesheet type="text/css">
<title>A.R.M.S. - Menu</title>
</head>

<body>

<table class=layout cellspacing=0>
<tr>

<td class=sidebar>
<img src="logo_s.jpg" class="logo"><br><br>
<a href="index.jsp" class=sideBarItem>Home</a>
<a href="accountsetup.jsp" class=sideBarItem>New Account</a>
<span class=currentBarItem>View Menu</span>
</td>


<td class=content>

<!-- This next line is the main part of this page. It ouputs the menu -->
<%= session.getAttribute("menuView")%>

</td>

</tr>
</table>
</body>