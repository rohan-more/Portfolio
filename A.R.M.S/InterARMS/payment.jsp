<html>
<head>
<link href="patron.css" rel=stylesheet type="text/css">
<title>A.R.M.S. - Menu</title>
</head>
<jsp:useBean id="payment" scope="session" class="cupojava.PaymentInfoBean" />
<script type="text/javascript">
/*============================================================================*/

/*

This routine checks the credit card number. The following checks are made:

1. A number has been provided
2. The number is a right length for the card
3. The number has an appropriate prefix for the card
4. The number has a valid modulus 10 number check digit if required

If the validation fails an error is reported.

The structure of credit card formats was gleaned from
    http://www.blackmarket-press.net/info/plastic/check_digit.htm 
where the details of other cards may also be found.

Parameters:
            cardnumber           number on the card
            cardname             name of card as defined in the card list below

Author:     John Gardner
Date:       1st November 2003

*/

/*
   If a credit card number is invalid, an error reason is loaded into the 
   global ccErrorNo variable. This can be be used to index into the global error  
   string array to report the reason to the user if required:
   
   e.g. if (!checkCreditCard (number, name) alert (ccErrors(ccErrorNo);
*/

var ccErrorNo = 0;
var ccErrors = new Array ()

ccErrors [0] = "Unknown card type";
ccErrors [1] = "No card number provided";
ccErrors [2] = "Credit card number is in invalid format";
ccErrors [3] = "Credit card number is invalid";
ccErrors [4] = "Credit card number has an inappropriate number of digits";

function checkCreditCard (cardnumber, cardname) {

     
  // Array to hold the permitted card characteristics
  var cards = new Array();

  // Define the cards we support. You may add addtional card types.
  
  //  Name:      As in the selection box of the form - must be same as user's
  //  Length:    List of possible valid lengths of the card number for the card
  //  prefixes:  List of possible prefixes for the card
  //  checkdigit Boolean to say whether there is a check digit
  
  cards [0] = {name: "Visa", 
               length: "13,16", 
               prefixes: "4",
               checkdigit: true};
  cards [1] = {name: "MasterCard", 
               length: "16", 
               prefixes: "51,52,53,54,55",
               checkdigit: true};
  cards [2] = {name: "DinersClub", 
               length: "14,", 
               prefixes: "300,301,302,303,304,305,36,38",
               checkdigit: true};
  cards [3] = {name: "CarteBlanche", 
               length: "14", 
               prefixes: "300,301,302,303,304,305,36,38",
               checkdigit: true};
  cards [4] = {name: "AmEx", 
               length: "15", 
               prefixes: "34,37",
               checkdigit: true};
               
  // Establish card type
  var cardType = -1;
  for (i=0; i<cards.length; i++) {

    // See if it is this card (ignoring the case of the string)
    if (cardname.toLowerCase () == cards[i].name.toLowerCase()) {
      cardType = i;
      break;
    }
  }
  
  // If card type not found, report an error
  if (cardType == -1) {
     ccErrorNo = 0;
     return false; 
  }
   
  // Ensure that the user has provided a credit card number
  if (cardnumber.length == 0)  {
     ccErrorNo = 1;
     return false; 
  }
  
  // Check that the number is numeric, although we do permit a space to occur  
  // every four digits. 
  var cardNo = cardnumber
  var cardexp = /^([0-9]{4})\s?([0-9]{4})\s?([0-9]{4})\s?([0-9]{1,4})$/;
  if (!cardexp.exec(cardNo))  {
     ccErrorNo = 2;
     return false; 
  }
    
  // Now remove any spaces from the credit card number
  cardexp.exec(cardNo);
  cardNo = RegExp.$1 + RegExp.$2 + RegExp.$3 + RegExp.$4;
       
  // Now check the modulus 10 check digit - if required
  if (cards[cardType].checkdigit) {
    var checksum = 0;                                  // running checksum total
    var mychar = "";                                   // next char to process
    var j = 1;                                         // takes value of 1 or 2
  
    // Process each digit one by one starting at the right
    for (i = cardNo.length - 1; i >= 0; i--) {
    
      // Extract the next digit and multiply by 1 or 2 on alternative digits.
      calc = Number(cardNo.charAt(i)) * j;
    
      // If the result is in two digits add 1 to the checksum total
      if (calc > 9) {
        checksum = checksum + 1;
        calc = calc - 10;
      }
    
      // Add the units element to the checksum total
      checksum = checksum + calc;
    
      // Switch the value of j
      if (j ==1) {j = 2} else {j = 1};
    } 
  
    // All done - if checksum is divisible by 10, it is a valid modulus 10.
    // If not, report an error.
    if (checksum % 10 != 0)  {
     ccErrorNo = 3;
     return false; 
    }
  }  

  // The following are the card-specific checks we undertake.
  var LengthValid = false;
  var PrefixValid = false; 
  var undefined; 

  // We use these for holding the valid lengths and prefixes of a card type
  var prefix = new Array ();
  var lengths = new Array ();
    
  // Load an array with the valid prefixes for this card
  prefix = cards[cardType].prefixes.split(",");
      
  // Now see if any of them match what we have in the card number
  for (i=0; i<prefix.length; i++) {
    var exp = new RegExp ("^" + prefix[i]);
    if (exp.test (cardNo)) PrefixValid = true;
  }
      
  // If it isn't a valid prefix there's no point at looking at the length
  if (!PrefixValid) {
     ccErrorNo = 3;
     return false; 
  }
    
  // See if the length is valid for this card
  lengths = cards[cardType].length.split(",");
  for (j=0; j<lengths.length; j++) {
    if (cardNo.length == lengths[j]) LengthValid = true;
  }
  
  // See if all is OK by seeing if the length was valid. We only check the 
  // length if all else was hunky dory.
  if (!LengthValid) {
     ccErrorNo = 4;
     return false; 
  };   
  
  // The credit card is in the required format.
  return true;
}





function testCreditCard () {
  myCardNo = document.getElementById('ccno').value;
  myCardType = document.getElementById('cctype').value;
  if (checkCreditCard (myCardNo,myCardType)) {
    alert ("Credit card has a valid format")
  } 
  else {
    alert (ccErrors[ccErrorNo]);
    document.getElementById('ccno').value= "";
  };
}

/*============================================================================*/
</script>
<body>
<% 
if (session.getAttribute("id")== null || session.getAttribute("id").equals(""))
    response.sendRedirect("index.jsp");
%>

<form action="PaymentServlet" method="get">
<table class=layout cellspacing=0>
<tr>

<td class=sidebar>
<img src="logo_s.jpg" class="logo"><br><br>
<a href="action.jsp" class=sideBarItem>Home</a>
<span class=currentBarItem>Make Payment</span>
<a href="SetRedirectServlet" class=sideBarItem>My Account</a>
<a href="SignOutServlet" class="sideBarItem">Sign Out</a><br>
</td>


<td class="content">




<!-- This next line is the main part of this page. It ouputs the menu -->
<%= session.getAttribute("menuOrder")%>
<% if (session.getAttribute("pricezero")!= null
        && session.getAttribute("pricezero").equals("1"))
        session.setAttribute("pricezero", "0");
   else{
%>
<div class="order">
<h2 align="Center">Enter Billing Information</h2>
<b>

<%=payment.flagName()%>Name on card: <input type="text" name="ccname" size="25" value="<jsp:getProperty name='payment' property='name' />"><br><br>
<%=payment.flagNumber()%>Card Number: <input type="text" name="ccno" size="16" value="<jsp:getProperty name='payment' property='number' />">&nbsp;&nbsp;
<%=payment.flagCompany()%>
<select size="1" name="cctype">
<option value='AmEx'>AmEx</option>
<option value='MasterCard'>MasterCard</option>
<option value='Visa'>Visa</option>
<option value='DinersClub'>DinersClub</option>
<option value='CarteBlanche'>CarteBlanche</option>
</select>

<br><br>

<%=payment.flagExpiration()%>Expiration Date (MM/YY): <input type="text" name="ccexp" size="5" value="<jsp:getProperty name='payment' property='expiration' />">&nbsp;&nbsp;&nbsp;
<%=payment.flagSecurity()%>Security Number: <input type="text" name="ccsec" size="3" value="<jsp:getProperty name='payment' property='security' />"><br><br>
<input type='submit' name='action' value='Submit' onclick='testCreditCard();'>
</b>
</div>

<%}%>
</td>
</tr>
</table>
</form>
</body>