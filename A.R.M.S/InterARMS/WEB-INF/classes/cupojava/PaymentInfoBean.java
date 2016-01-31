/*
 * PaymentInfoBean.java
 *
 * Created on October 6, 2004, 10:23 PM
 * Modified on October 6, 2004, 11:03 PM
 */

package cupojava;

//import java.util.*;
import java.sql.*;
/**
 *
 * @author  Erik Toussaint
 */
public class PaymentInfoBean {
    /** Bean variables */
    private String name, number, company, expiration, security;
    
    DBFunctions db = new DBFunctions();
    
    /** Creates a new instance of accountInfoBean */
    public PaymentInfoBean() {
    }
    
    /** Accessor Method for Name on Card */
    public String getName() {
        return StringEditor.nullEraser(name);
    }
    
    /** Accessor Method for Card Number */
    public String getNumber() {
        return StringEditor.nullEraser(number);
    }
    
    /** Accessor Method for Card Company */
    public String getCompany() {
        return StringEditor.nullEraser(company);
    }
    
    /** Accessor Method for Card Expiration */
    public String getExpiration() {
        return StringEditor.nullEraser(expiration);
    }

    /** Accessor Method for Card Security Number */
    public String getSecurity() {
        return StringEditor.nullEraser(security);
    }
    
    /** Mutator Method for Name on Card */
    public void setName(String newName){
        name= newName;
    }
    
    /** Mutator Method for Card Number */
    public void setNumber(String newNumber){
        number= newNumber;
    }
    
    /** Mutator Method for Card Company */
    public void setCompany(String newCompany){
        company= newCompany;
    }
    
    /** Mutator Method for Card Expiration */
    public void setExpiration(String newExpiration){
        expiration= newExpiration;
    }
    
    /** Mutator Method for Card Security Number */
    public void setSecurity(String newSecurity){
        security= newSecurity;
    }
 
    
    /** If Name on Card is not valid this outputs <span class="red">*</span> */
    public String flagName(){
        String temp= "";
        if (this.getName()== null || this.getName().equals(""))
            temp= "<span class='red'>*</span>";
        else if (!StringEditor.isLetter(this.getName()))
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** If Card Security Number is not valid this outputs <span class="red">*</span> */
    public String flagSecurity(){
        String temp= "";
        if (this.getSecurity()== null || this.getSecurity().equals(""))
            temp= "<span class='red'>*</span>";
      
        else if (!StringEditor.isInteger(this.getSecurity()))
            temp= "<span class=\"red\">*</span>";
       
        
        return temp;
    }
    
 
    
    /** If Card Company name is not valid this outputs <span class="red">*</span> */
    public String flagCompany(){
        String temp= "";
        if (this.getCompany()== null || this.getCompany().equals(""))
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** If Card Expiration Date is not valid this outputs <span class="red">*</span> */
    public String flagExpiration(){
        String temp= "";
        try{
        if (this.getExpiration()== null || this.getExpiration().equals(""))
            temp= "<span class='red'>*</span>";
        else if (!StringEditor.isInteger(this.getExpiration()))
            temp= "<span class='red'>*</span>";
        else if (!StringEditor.isValidExpiration(Integer.parseInt(this.getExpiration())))
            temp= "<span class='red'>*</span>";
        }catch (Exception e){temp= "<span class='red'>*</span>";}
        
        return temp;
    }
    
    /** If Card Number is not valid this outputs <span class="red">*</span> */
    public String flagNumber(){
        String temp= "";
        if (this.getNumber()== null || this.getNumber().equals(""))
            temp= "<span class='red'>*</span>";
        else if (!StringEditor.isDigit(this.getNumber()))
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** Converts MMYY integer to a Date Object */
    public java.sql.Date expDate(){
        int d= 0;
        int month= 0;
        int year= 0;
        try{
            d= Integer.parseInt(this.getExpiration());
            month= d/100-1;
            year= 100 + d- ((month+1)*100);
        }catch(Exception e){}
        Date dt= new Date(year, month, 01);
        return dt;
    }

}
