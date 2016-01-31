<html>
<head>
<link href="intranet.css" rel=stylesheet type="text/css">
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
<span class="currentBarItem">Home</span>
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
<a href="AccountManagementServlet" class="sideBarItem">Account Management</a><br />
<%}%>
<a href="MenuManagementServlet" class="sideBarItem">Menu Management</a><br />
<%} else response.sendRedirect("/arms");%>
<a href="SignOutServlet" class="sideBarItem">Sign Out</a><br>

</td>


<td class=content>

<!-- This next line is the main part of this page. It ouputs the menu -->
<span class='red'>
<%if (session.getAttribute("acceditsuccess")!= null){%>
<%=session.getAttribute("acceditsuccess")%>
<%session.setAttribute("acceditsuccess", "");}%>
<%if (session.getAttribute("menucreatesuccess")!= null){%>
<%=session.getAttribute("menucreatesuccess")%>
<%session.setAttribute("menucreatesuccess", "");}%>
</span>
<h2 align="center">Welcome to A.R.M.S. 
<span class="blue"><%=session.getAttribute("firstName")%></span>!</h2><br> 
<%@ include file="news.jsp" %>
</td>
</tr>
</table>
</form>
</body>
</html>

