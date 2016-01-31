/*
 * StringEditor.java
 *
 * Created on October 9, 2004, 5:52 PM
 */

package cupojava;
import java.util.*;

/**
 *
 * @author  Erik Toussaint
 */
public class StringEditor {
    
    /** Creates a new instance of Validator */
    public StringEditor() {
    }
    
    /** Returns list of bad characters */
    public static String badCharList(){
        String badcharlist= "!@$%^&*-_+={}()/][:;\"'<>\\|~`";
        return badcharlist;
    }
    
    /** Returns list of bad characters for an email format (excluding '@') */
    public static String nonEmailChars(){
        String badcharlist= "!#$%^&*-_+={}()][:;\"'<>?/\\|~`";
        return badcharlist;
    }
    
    /** Replaces a null object/string to an empty string */
    public static String nullEraser(Object o){
        if (o== null || o.toString()== null || o.toString().trim().equals(""))
            return "";
        else 
            return o.toString();
    }
    
    /** Returns true if String is an integer */
    public static boolean isInteger(String s){
        try {
            Integer.parseInt(""+ s);
            return true;
        }
        catch (Exception e){
            return false;
        }
    }
    
    /** Returns true if String is a double */
    public static boolean isDouble(String s){
        try {
            Double.parseDouble(""+ s);
            return true;
        }
        catch (Exception e){
            return false;
        }
    }
    
    /** Returns true if String is made of letters, caps and lower case alike */
    public static boolean isLetter(String s){
        for (int x= 0; x< s.length(); x++){
            if (!Character.isLetter(s.charAt(x)) && s.charAt(x)!= ' ')
                return false;
        }
        return true;
    }
    
     /** Returns true if String is made of only digits */
    public static boolean isDigit(String s){
        for (int x= 0; x< s.length(); x++){
            try{
                Integer.parseInt(""+s.charAt(x));
            }catch(Exception e) {return false;}
        }
        return true;
    }
    
    /** Returns true if String is made of only letters and digits */
    public static boolean isDigitLetter(String s){
        for (int x= 0; x< s.length(); x++){
            if (!Character.isLetter(s.charAt(x)) && !Character.isDigit(s.charAt(x)))
                return false;
        }
        return true;
    }
    
    /** Removes any substring x from a string y and returns the resulting string*/
    public static String removeXfromY(String x, String y){
        String temp= "";
        int begin= 0;
        int index= y.indexOf(x);
        
        while (index!= -1) {
            for (int a= begin ; a< index+ x.length()+1; a++)
            {
                if (a< index){
                    temp= temp+ y.charAt(a);
                }
                begin= a;
            }
            index= y.indexOf(x, begin);
        }
        for (int b= begin; b< y.length(); b++)
        {
            temp= temp+ y.charAt(b);
        }
        return temp;
    }
    
    /** Removes every characters found in string x from y and returns resulting string 
    *   For exampe StringEditor.removeXXXfromY("bs", "because")= "ecaue"
    */
    public static String removeXXXfromY(String x, String y){
        String temp= y;
        for (int a= 0; a< x.length(); a++){
            temp= removeXfromY(""+ x.charAt(a), temp); 
        }
        return temp;
    }
    
    /** Removes most non letter and non digit characters from string y and returns resulting string 
    *   For exampe StringEditor.removeBadCharsfromString("because123#$%^&*")= "because123"
    */
    public static String removeBadCharsfromString(String y){
        String temp= y;
        temp= removeXXXfromY(badCharList(), temp); 
        return temp;
    }
    
    /** Removes most non letter and non digit excluding '@' characters from string y and returns resulting string 
    *   For exampe StringEditor.removeBadCharsfromEmail("xxx$%^&@%^mail.com")= "xxx@mail.com"
    */
    public static String removeBadCharsfromEmail(String y){
        String temp= y;
        temp= removeXXXfromY(nonEmailChars(), temp); 
        return temp;
    }
    
    /** Returns true if Expiration Date (credit card) is valid. It is valid only
     * if it is a valid date and that date is a future date, not a past one
     */
    public static boolean isValidExpiration(int d)
    {
        if (d< 101 || d>1231)
            return false;
        int month= d/100-1;
        int year= 100 + d- ((month+1)*100);
        
        Date now= new Date();
        Date date= new Date();
        
        try{
        date= new Date(year, month, 1);
        }catch (Exception e){ 
            return false;
        }
        
        if (date.getYear()< now.getYear())
            return false;
        
        if (date.getYear()== now.getYear() && date.getMonth()< now.getMonth())
            return false;
        
        return true;
    }
    
}
