<html>
<head>
<link href="patron.css" rel=stylesheet type="text/css">
<title>A.R.M.S. - Welcome!</title>
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
<span class="currentBarItem">Home</span><br>
<a href="MenuServlet" class="sideBarItem">Order</a><br>
<a href="SetRedirectServlet" class=sideBarItem>My Account</a>
<a href="SignOutServlet" class="sideBarItem">Sign Out</a><br>
</td>


<td class="content">

<!-- This next line is the main part of this page. It ouputs the menu -->
<span class='red'>
<%if (session.getAttribute("acceditsuccess")!= null){%>
<%=session.getAttribute("acceditsuccess")%>
<%session.setAttribute("acceditsuccess", "");}%>
</span>
<br><br>
<h2 align="center">Welcome to A.R.M.S. 
<span class="blue"><%=session.getAttribute("firstName")%></span>!</h2><br>
<%@ include file="news.jsp" %>

</td>

</tr>
</table>
</form>
</body>
</html>

