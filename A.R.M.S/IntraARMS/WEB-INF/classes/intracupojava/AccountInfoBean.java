/*
 * AccountInfoBean.java
 *
 * Created on October 6, 2004, 10:23 PM
 * Modified on October 6, 2004, 11:03 PM
 */

package intracupojava;

import java.sql.*;
/**
 *
 * @author  Erik Toussaint
 */
public class AccountInfoBean {
    /** Bean variables */
    private String id, login, password, password2, type;
    private String firstName, middleName, lastName;
    private String email, phone, age;
    private String address1, address2, city, state, zip;
    DBFunctions db = new DBFunctions();
    
    /** Creates a new instance of accountInfoBean */
    public AccountInfoBean() {
    }
    
    /** Accessor Method for ID */
    public String getId() {
        return StringEditor.nullEraser(id);
    }
    
    /** Accessor Method for LoginID */
    public String getLogin() {
        return StringEditor.nullEraser(login);
    }
    
    /** Accessor Method for Password */
    public String getPassword() {
        return StringEditor.nullEraser(password);
    }
    
    /** Accessor Method for double check Password2 */
    public String getPassword2() {
        return StringEditor.nullEraser(password2);
    }
    
    /** Accessor Method for Type
     *[s=superadmin, m=manager, e=employee, p=patron, o=other
     */
    public String getType() {
        return StringEditor.nullEraser(type);
    }
    
    /** Accessor Method for First Name */
    public String getFirstName() {
        return StringEditor.nullEraser(firstName);
    }
    
    /** Accessor Method for Middle Name */
    public String getMiddleName() {
        return StringEditor.nullEraser(middleName);
    }
    
    /** Accessor Method for Last Name */
    public String getLastName() {
        return StringEditor.nullEraser(lastName);
    }
    
    /** Accessor Method for Email */
    public String getEmail() {
        return StringEditor.nullEraser(email);
    }
    
    /** Accessor Method for Phone Number */
    public String getPhone() {
        return StringEditor.nullEraser(phone);
    }
    
    /** Accessor Method for Address Line 1 */
    public String getAddress1() {
        return StringEditor.nullEraser(address1);
    }
    
    /** Accessor Method for Address Line 2 */
    public String getAddress2() {
        return StringEditor.nullEraser(address2);
    }
    
    /** Accessor Method for City */
    public String getCity() {
        return StringEditor.nullEraser(city);
    }
    
    /** Accessor Method for State */
    public String getState() {
        return StringEditor.nullEraser(state);
    }
    
    /** Accessor Method for Zip Code */
    public String getZip() {
        return StringEditor.nullEraser(zip);
    }
    
    /** Accessor Method for Age */
    public String getAge() {
        return StringEditor.nullEraser(age);
    }
    
   
    /** Mutator Method for ID */
    public void setId(String newId){
        id= newId;
    }
    
    /** Mutator Method for Login Name */
    public void setLogin(String newLogin){
        login= newLogin;
    }
    
    /** Mutator Method for Password */
    public void setPassword(String newPassword){
        password= newPassword;
    }
    
    /** Mutator Method for Password2 */
    public void setPassword2(String newPassword2){
        password2= newPassword2;
    }
    
    /** Mutator Method for Type */
    public void setType(String newType){
        type= newType;
    }
    
    /** Mutator Method for First Name */
    public void setFirstName(String newFirstName){
        firstName= StringEditor.nullEraser(newFirstName);
    }
    
    /** Mutator Method for Middle Name */
    public void setMiddleName(String newMiddleName){
        middleName= StringEditor.nullEraser(newMiddleName);
    }
    
    /** Mutator Method for Last Name */
    public void setLastName(String newLastName){
        lastName= StringEditor.nullEraser(newLastName);
    }
    
    /** Mutator Method for Email */
    public void setEmail(String newEmail){
        email= StringEditor.nullEraser(newEmail);
    }
    
    /** Mutator Method for Phone Number */
    public void setPhone(String newPhone){
        phone= StringEditor.nullEraser(newPhone);
    }
    
    /** Mutator Method for Address Line 1 */
    public void setAddress1(String newAddress1){
        address1= StringEditor.nullEraser(newAddress1);
    }
    
    /** Mutator Method for Address Line 2 */
    public void setAddress2(String newAddress2){
        address2= StringEditor.nullEraser(newAddress2);
    }
    
    /** Mutator Method for City */
    public void setCity(String newCity){
        city= StringEditor.nullEraser(newCity);
    }
    
    /** Mutator Method for State */
    public void setState(String newState){
        state= StringEditor.nullEraser(newState);
    }
    
    /** Mutator Method for Zip Code */
    public void setZip(String newZip){
        zip= newZip;
    }

    /** Mutator Method for Age */
    public void setAge(String newAge){
        age= newAge;
    }
    
    /** If type is not valid this outputs <span class="red">*</span> */
    public String flagType(){
        String temp= "";
        if (this.getType()== null || this.getType().equals(""))
            temp= "<span class='red'>*</span>";
        else if (!this.getType().toLowerCase().equals("a")
            && !this.getType().toLowerCase().equals("m")
            && !this.getType().toLowerCase().equals("e")
            && !this.getType().toLowerCase().equals("p"))
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** If login is not valid this outputs <span class="red">*</span> */
    public String flagLogin(){
        String temp= "";
        if (this.getLogin()== null || this.getLogin().equals(""))
            temp= "<span class='red'>*</span>";
        db.connectDB();
        String query= "SELECT * FROM Account_Information WHERE AI_ID='" +
                        this.getId()+ "'";
        
        ResultSet r = db.get_query(query);
        try{
        if (r.next()== true)
            temp= "<span class=\"red\">*</span>";
        }catch(Exception e){System.out.println(e);}
        
        return temp;
    }
    
    /** If password is not valid this outputs <span class="red">*</span> */
    public String flagPassword(){
        String temp= "";
        if (this.getPassword()== null || this.getPassword().equals(""))
            temp= "<span class='red'>*</span>";
        if (!this.getPassword().equals(this.getPassword2()))
            temp= "<span class='red'>*</span>";
        if (temp!= null && !temp.equals(""))
        {
            this.setPassword("");
            this.setPassword2("");
        }
        return temp;
    }
    
    /** If first name is not valid this outputs <span class="red">*</span> */
    public String flagFirstName(){
        String temp= "";
        if (this.getFirstName()== null || this.getFirstName().equals(""))
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** If last name is not valid this outputs <span class="red">*</span> */
    public String flagLastName(){
        String temp= "";
        if (this.getLastName()== null || this.getLastName().equals(""))
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** If address1 is not valid this outputs <span class="red">*</span> */
    public String flagAddress1(){
        String temp= "";
        if (this.getAddress1()== null || this.getAddress1().equals(""))
            temp= "<span class='red'>*</span>";
        
        return temp;
    }

    /** If address2 is not valid this outputs <span class="red">*</span> */
    public String flagAddress2(){
        String temp= "";
        if (this.getAddress2()== null || this.getAddress2().equals(""))
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** If city is not valid this outputs <span class="red">*</span> */
    public String flagCity(){
        String temp= "";
        if (this.getCity()== null || this.getCity().equals("")
            || !StringEditor.isLetter(this.getCity()))
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** If state is not valid this outputs <span class="red">*</span> */
    public String flagState(){
        String temp= "";
        if (this.getState()== null || this.getState().equals("")
            || !StringEditor.isLetter(this.getState()) || this.getState().length()!= 2)
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** If email is not valid this outputs <span class="red">*</span> */
    public String flagEmail(){
        String temp= "";
        if (this.getEmail()== null || this.getLogin().equals(""))
            temp= "<span class='red'>*</span>";
        else if (this.getEmail().indexOf("@")==-1 || this.getEmail().indexOf(".")==-1)
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** If zip is not valid this outputs <span class="red">*</span> */
    public String flagZip(){
        String temp= "";
        try{
        if (this.getZip()== null || this.getZip().equals("") 
            || !StringEditor.isInteger(this.getZip()) 
            || Integer.parseInt(this.getZip())< 0)
            temp= "<span class='red'>*</span>";
        }catch(Exception e){temp= "<span class='red'>*</span>";}
        
        return temp;
    }
    
    /** If phone is not valid this outputs <span class="red">*</span> */
    public String flagPhone(){
        String temp= "";
        
        if (this.getPhone()== null || this.getPhone().equals(""))
            temp= "<span class='red'>*</span>";
        if (this.getPhone()!= null && !this.getPhone().equals("") && !StringEditor.isDouble(this.getPhone()))
            temp= "<span class='red'>*</span>";
        if (this.getPhone()!= null && !this.getPhone().equals("") 
            && StringEditor.isDouble(this.getPhone()) && Double.parseDouble(this.getPhone())<999999999)
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** If age is not valid this outputs <span class="red">*</span> */
    public String flagAge(){
        String temp= "";
        if (this.getAge()!= null && !this.getAge().equals("") && !StringEditor.isInteger(""+ this.getAge()))
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
/**
  private String id, login, password, password2;
  private String firstName, middleName, lastName;
  private String email, phone;
  private String address1, address2, city, state;
  private char type, gender;
  private int zip, age, bdDay, bdMonth, bdYear;
 */
}
