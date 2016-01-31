<html>
<head>
<link href="patron.css" rel=stylesheet type="text/css">
<title>A.R.M.S. - Menu</title>
</head>

<body>

<jsp:useBean id="billing" scope="session" class="cupojava.BillingBean" />
<form action="LoginServlet" method="get">
<table class=layout cellspacing=0>
<tr>

<td class=sidebar>
<img src="logo_s.jpg" class="logo"><br><br>
<a href="index.jsp" class=sideBarItem>Home</a>
<span class=currentBarItem>Make Payment</span>
<a href="SetRedirectServlet" class=sideBarItem>My Account</a>
<a href="SignOutServlet" class="sideBarItem">Sign Out</a><br>
</td>


<td class="content">




<!-- This next line is the main part of this page. It ouputs the menu -->
<%= session.getAttribute("menuOrder")%>
<div class="order">
<h2 align="Center">Enter Billing Information</h2>
<b>

Name on card: <input type="text" name="card_name" size="25"><br><br>
Card Number: <input type="text" name="card_num" size="16">&nbsp;&nbsp;

<select size="1" name="card_type">
<option>AMEX</option>
<option>MasterCard</option>
<option>Visa</option>
</select>

<br><br>

Expiration Date (MM/YY): <input type="text" name="card_exp" size="5">&nbsp;&nbsp;&nbsp;
Security Number: <input type="text" name="card_sec" size="3"><br><br>
<input type='submit' name='action' value='Submit'>
</b>
</div>


</td>
</tr>
</table>
</form>
</body>