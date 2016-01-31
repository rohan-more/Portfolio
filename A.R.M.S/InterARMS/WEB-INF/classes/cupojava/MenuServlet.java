/*
 * MenuServlet.java
 *
 * Created on November 18, 2004, 3:57 PM
 */

package cupojava;

import java.io.*;
import java.net.*;

import javax.servlet.*;
import javax.servlet.http.*;
import java.sql.*;
/**
 *
 * @author  toussainte
 * @version
 */
public class MenuServlet extends HttpServlet {
    
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
        
        db.connectDB();
        String query= "SELECT * FROM Menu_Items WHERE M_Availability > 0";
        
        ResultSet r = db.get_query(query);
        
        String temp= "";
        try{
        while (r.next()){
            temp= temp + "<div class=\"menuItem\">"+
                  "<b>" + r.getString("M_Name")+ "</b><br>\n"+
                  r.getString("M_Description")+ "<br><br>\n"+
                  "<div align='right'>$"+ (double)((int)(Float.parseFloat(r.getString("M_Price"))*100))/100+ "<br>\n"+
                  "<i>Order <input type='text' maxlength='2' size='2' name='"+ r.getString("M_ID")+ 
                  "'> of this item</i></div>\n" + 
                  "</div>";
        }
        }catch (Exception e){System.out.println(e);}
        
        session.setAttribute("menuItems", temp);
        response.sendRedirect("menu.jsp");
        
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
