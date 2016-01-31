/*
 * AccountEditServlet.java
 *
 * Created on November 24, 2004, 6:47 PM
 */
package intracupojava;



import java.io.*;
import java.net.*;

import javax.servlet.*;
import javax.servlet.http.*;
import java.sql.*;
/**
 *
 * @author  Erik Toussaint
 * @version 1.0
 */
public class AccountEditServlet extends HttpServlet {
    
    DBFunctions db = new DBFunctions();
    /** Initializes Beans and variables.
     */
    public AccountInfoBean account;
    
    
    /** Initializes the servlet.
     */
    public void init(ServletConfig config) throws ServletException {
        super.init(config);
        
    }
    
    /** Destroys the servlet.
     */
    public void destroy() {
        
    }
    
    /** Processes requests for both HTTP <code>GET</code> and <code>POST</code> methods.
     * @param request servlet request
     * @param response servlet response
     */
    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
    throws ServletException, IOException {
        response.setContentType("text/html");
        PrintWriter out = response.getWriter();
        HttpSession session= request.getSession();
        
        //initialize account bean
        if (session.isNew()== true || session.getAttribute("account")== null)
            account= new AccountInfoBean();
        else if (session.isNew()== false && session.getAttribute("account")!= null)
            account= (AccountInfoBean)session.getAttribute("account");
        
        
        try{
        account.setFirstName(StringEditor.removeBadCharsfromString(request.getParameter("firstName")));
        System.out.println("The error is here");
        account.setMiddleName(StringEditor.removeBadCharsfromString(request.getParameter("middleName")));
        account.setLastName(StringEditor.removeBadCharsfromString(request.getParameter("lastName")));
        //account.setLogin(StringEditor.removeBadCharsfromString(request.getParameter("login")));
        //account.setType("P");
        account.setPassword(StringEditor.removeBadCharsfromString(request.getParameter("password")));
        account.setPassword2(StringEditor.removeBadCharsfromString(request.getParameter("password2")));
        account.setEmail(StringEditor.removeBadCharsfromEmail(request.getParameter("email")));
        account.setPhone(StringEditor.removeBadCharsfromString(request.getParameter("phone")));
        account.setAge(StringEditor.removeBadCharsfromString(request.getParameter("age")));
        account.setAddress1(request.getParameter("address1"));
        account.setAddress2(request.getParameter("address2"));
        account.setCity(StringEditor.removeBadCharsfromString(request.getParameter("city")));
        account.setState(StringEditor.removeBadCharsfromString(request.getParameter("state")));
        account.setZip(StringEditor.removeBadCharsfromString(request.getParameter("zip")));
        
        } catch (Exception e){System.out.println("11"+ e.getMessage());}
        
        
        session.setAttribute("account", account);
        
       
        db.connectDB();
        String query= "SELECT * FROM Account_Information WHERE AI_ID='"
        + account.getLogin()+ "'";
        
        
        if (account.flagLogin().equals("") && account.flagPassword().equals("")
        && account.flagFirstName().equals("") && account.flagLastName().equals("")
        && account.flagEmail().equals("") && account.flagAddress1().equals("")
        && account.flagCity().equals("") && account.flagState().equals("")
        && account.flagZip().equals("") && account.flagPhone().equals("")
        && account.flagAge().equals("") && account.flagType().equals("")){
        
            db.connectDB();
        try{
            query= "UPDATE Account_Information "+
            "SET AI_Password='"+account.getPassword()+"' , "+ 
            "AI_Type='"+account.getType().toUpperCase()+"' , "+
            "AI_First_Name='"+account.getFirstName()+"' , "+
	    "AI_Mid_Name='"+account.getMiddleName()+"' , "+
            "AI_Last_Name='"+account.getLastName()+"' , "+
            "AI_EMail='"+account.getEmail()+"' , "+
            "AI_Phone='"+account.getPhone()+"' , "+
            "AI_Address1='"+account.getAddress1()+"' , "+
            "AI_Address2='"+account.getAddress2()+"' , "+
            "AI_City='"+account.getCity()+"' , "+
            "AI_State='"+account.getState()+"' , "+
            "AI_Zip="+Integer.parseInt(account.getZip())+" , "+
            "AI_Age="+Integer.parseInt(account.getAge())+ " "+
            "WHERE AI_ID='"+account.getLogin()+"'";
        }catch(Exception e){System.out.println(e);}
            
        System.out.println(query);
        db.update_query(query);
        
        response.sendRedirect("action.jsp");
      }
        
        else{
            response.sendRedirect("accountedit.jsp");
        }
        out.close();
    }
    
    /** Handles the HTTP <code>GET</code> method.
     * @param request servlet request
     * @param response servlet response
     */
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
    throws ServletException, IOException {
        processRequest(request, response);
    }
    
    /** Handles the HTTP <code>POST</code> method.
     * @param request servlet request
     * @param response servlet response
     */
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
    throws ServletException, IOException {
        processRequest(request, response);
    }
    
    /** Returns a short description of the servlet.
     */
    public String getServletInfo() {
        return "This is the main servlet";
    }
    
}
