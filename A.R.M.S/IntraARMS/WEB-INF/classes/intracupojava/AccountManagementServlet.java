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
public class AccountManagementServlet extends HttpServlet {
    
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
            String query= "SELECT * FROM Account_Information ORDER BY AI_Type, AI_First_Name";
            
            ResultSet r = db.get_query(query);
            String accountlist= "<select name='accountlist' size='1'>"
                                + "<option value='newarmsid'>Create new account</option>";
            while (r.next()){
                String tempb= "";
                if (r.getString("AI_Type").toLowerCase().equals("m"))
                    tempb= "Manager";
                if (r.getString("AI_Type").toLowerCase().equals("e"))
                    tempb= "Employee"; 
                if (r.getString("AI_Type").toLowerCase().equals("a"))
                    tempb= "Admin";
                if (r.getString("AI_Type").toLowerCase().equals("s"))
                    tempb= "SuperAdmin";
                if (r.getString("AI_Type").toLowerCase().equals("p"))
                    tempb= "Patron";
                
                accountlist+= "<option value='"+ r.getString("AI_ID")+"'>"
                            + "Edit/Delete <b>"+ r.getString("AI_First_Name")+ " "
                            + r.getString("AI_Last_Name")
                            + "</b>'s account ("+ tempb+ ")</option>";
            }
            accountlist+= "</select>";
            session.setAttribute("accountlist", accountlist);
            }catch(Exception e){System.out.println("99"+ e);}
         
            if (request.getParameter("hidden")!= null 
                && request.getParameter("hidden").equals("y"))
            {
                try{
                    db.connectDB();
                    String query= "SELECT * FROM Account_Information WHERE AI_ID='"
                                  + request.getParameter("accountlist")+ "'";
            
                    ResultSet r = db.get_query(query); 
                    if (r.next())
                    {
                        AccountInfoBean account= new AccountInfoBean();
                        String login= " "+ StringEditor.nullEraser(r.getString("AI_ID"));
                        String password= " "+ StringEditor.nullEraser(r.getString("AI_Password"));
                        String type= " "+ StringEditor.nullEraser(r.getString("AI_Type"));
                        String firstName= " "+ StringEditor.nullEraser(r.getString("AI_First_Name"));
                        String middleName= " "+ StringEditor.nullEraser(r.getString("AI_Mid_Name"));
                        String lastName= " "+ StringEditor.nullEraser(r.getString("AI_Last_Name"));
                        String email= " "+ StringEditor.nullEraser(r.getString("AI_EMail"));
                        String phone= " "+ StringEditor.nullEraser("" + r.getString("AI_Phone"));
                        String age= " "+ StringEditor.nullEraser(r.getString("AI_Age"));
                        String address1= " "+ StringEditor.nullEraser(r.getString("AI_Address1"));
                        String address2= " "+ StringEditor.nullEraser(r.getString("AI_Address2"));
                        String city= " "+ StringEditor.nullEraser(r.getString("AI_City"));
                        String state= " "+ StringEditor.nullEraser(r.getString("AI_State"));
                        String zip= " "+ StringEditor.nullEraser(""+ r.getInt("AI_Zip"));
                
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
                        session.setAttribute("account2", account);
                        response.sendRedirect("accountedit2.jsp");
                    }
                    
                    else if (request.getParameter("accountlist")!= null
                        && request.getParameter("accountlist").equals("newarmsid"))
                        response.sendRedirect("accountsetup.jsp");
                    
                    //else response.sendRedirect("accountedit2.jsp");   
                }catch (Exception e){System.out.println("74"+ e);}
                
                //response.sendRedirect("accountedit2.jsp");
            }
            else             
                response.sendRedirect("accountmanagement.jsp");
        
        
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
