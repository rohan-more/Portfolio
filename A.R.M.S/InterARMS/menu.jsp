<html>
<head>
<link href="patron.css" rel=stylesheet type="text/css">
<title>A.R.M.S. - Menu</title>
</head>

<body>
<% 
if (session.getAttribute("id")== null || session.getAttribute("id").equals(""))
    response.sendRedirect("index.jsp");
%>
<form action="OrderServlet" method="get">
<table class=layout cellspacing=0>
<tr>

<td class=sidebar>
<img src="logo_s.jpg" class="logo"><br><br>
<a href="action.jsp" class=sideBarItem>Home</a>
<span class=currentBarItem>Order</span>
<a href="SetRedirectServlet" class=sideBarItem>My Account</a>
<a href="SignOutServlet" class="sideBarItem">Sign Out</a><br>
</td>


<td class=content>

<!-- This next line is the main part of this page. It ouputs the menu -->
<%= session.getAttribute("menuItems")%>
<table width="100%"><tr><td>&nbsp;</td><td>&nbsp;</td>
<td align="right">
<input type='submit' name='action' value='Submit'>
</td></tr></table>
</td>

</tr>
</table>
</form>
</body>