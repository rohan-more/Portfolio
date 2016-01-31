/*
 * AccountServlet.java
 *
 * Created on November 9, 2004, 2:22 PM
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
public class AccountServlet extends HttpServlet {
    
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
        account.setMiddleName(StringEditor.removeBadCharsfromString(request.getParameter("middleName")));
        account.setLastName(StringEditor.removeBadCharsfromString(request.getParameter("lastName")));
        account.setLogin(StringEditor.removeBadCharsfromString(request.getParameter("login")));
        account.setType(StringEditor.removeBadCharsfromString(request.getParameter("type")));
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
        
        } catch (Exception e){System.out.println(e.getMessage());}
        
        
        session.setAttribute("account", account);      
        
        if (account.flagLogin().equals("") && account.flagPassword().equals("")
        && account.flagFirstName().equals("") && account.flagLastName().equals("")
        && account.flagEmail().equals("") && account.flagAddress1().equals("")
        && account.flagCity().equals("") && account.flagState().equals("")
        && account.flagZip().equals("") && account.flagPhone().equals("")
        && account.flagAge().equals("") && account.flagType().equals("")){
        
            db.connectDB();
            String query= "";
        try{
            query= "INSERT INTO Account_Information (AI_ID, AI_Password, "
            +"AI_Type, AI_First_Name, AI_Mid_Name, AI_Last_Name, AI_EMail, "
            +"AI_Phone, AI_Address1, AI_Address2, AI_City, AI_State, AI_Zip, "
            +"AI_Age) VALUES " 
            +"('"+account.getLogin()+"', '"+account.getPassword()+"', '"
            +account.getType().toUpperCase()+"', '"+account.getFirstName()+"', '"
            +account.getMiddleName()+"', '"+account.getLastName()+"', '"
            +account.getEmail()+"', '"+account.getPhone()+"', '"
            +account.getAddress1()+"', '"+account.getAddress2()+"', '"
            +account.getCity()+"', '"+account.getState()+"', "
            +Integer.parseInt(account.getZip())+", "
            +Integer.parseInt(account.getAge())+ ")";
        }catch(Exception e){System.out.println(e);}
        System.out.println(query);
        db.update_query(query);
        
        response.sendRedirect("action.jsp");
      }
        
        else{
            String temp2= "";
            if (account.flagLogin().equals("<span class=\"red\">*</span>")){
                temp2= "Login is already in used in another account, pick another one";
                session.setAttribute("logininuse", temp2);
            }
            response.sendRedirect("accountsetuperror.jsp");
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
