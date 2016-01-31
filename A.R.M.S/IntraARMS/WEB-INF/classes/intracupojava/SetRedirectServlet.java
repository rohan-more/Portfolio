/*
 * SetRedirectServlet.java
 *
 * Created on November 24, 2004, 3:53 PM
 */

package intracupojava;

import java.io.*;
import java.net.*;

import javax.servlet.*;
import javax.servlet.http.*;

import java.sql.*;
/**
 *
 * @author  Owner
 * @version
 */
public class SetRedirectServlet extends HttpServlet {
    
    DBFunctions db = new DBFunctions();

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
        
            try{
            db.connectDB();
            String query= "SELECT * FROM Account_Information WHERE AI_ID='" +
            session.getAttribute("id")+ "'";
            
            ResultSet r = db.get_query(query);
            AccountInfoBean account= new AccountInfoBean();
            
            while(r.next()){
                String login= " "+ r.getString("AI_ID");
                String password= " "+ r.getString("AI_Password");
                String type= " "+ r.getString("AI_Type");
                String firstName= " "+ r.getString("AI_First_Name");
                String middleName= " "+ r.getString("AI_Mid_Name");
                String lastName= " "+ r.getString("AI_Last_Name");
                String email= " "+ r.getString("AI_EMail");
                String phone= " "+ r.getString("AI_Phone");
                String age= " "+ r.getString("AI_Age");
                String address1= " "+ r.getString("AI_Address1");
                String address2= " "+ r.getString("AI_Address2");
                String city= " "+ r.getString("AI_City");
                String state= " "+ r.getString("AI_State");
                String zip= " "+ r.getInt("AI_Zip");
                
                account.setLogin(login.trim());
                account.setPassword(password.trim());
                account.setPassword2(password.trim());
                account.setType(type.trim());
                account.setFirstName(firstName.trim());
                account.setMiddleName(middleName.trim());
                account.setLastName(lastName.trim());
                account.setEmail(email.trim());
                account.setPhone(phone.trim());
                account.setAge(age.trim());
                account.setAddress1(address1.trim());
                account.setAddress2(address2.trim());
                account.setCity(city.trim());
                account.setState(state.trim());
                account.setZip(zip.trim());
                session.setAttribute("account", account);
            }
            }catch(Exception e){System.out.println(e);}
        
            response.sendRedirect("accountedit.jsp");
        
        
        /* TODO output your page here
        out.println("<html>");
        out.println("<head>");
        out.println("<title>Servlet</title>");
        out.println("</head>");
        out.println("<body>");
         
        out.println("</body>");
        out.println("</html>");
         */
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
        return "Short description";
    }
    
}
