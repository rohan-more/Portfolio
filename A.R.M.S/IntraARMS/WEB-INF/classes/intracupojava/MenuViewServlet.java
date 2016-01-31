/*
 * MenuViewServlet.java
 *
 * Created on November 23, 2004, 4:27 PM
 */

package intracupojava;

import java.io.*;
import java.net.*;

import javax.servlet.*;
import javax.servlet.http.*;
import java.sql.*;
/**
 *
 * @author  cheungj
 * @version
 */
public class MenuViewServlet extends HttpServlet {
    
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
        String query= "SELECT * FROM Menu_Items WHERE M_Availability > 0"
        + " ORDER BY M_Name";
        
        ResultSet r = db.get_query(query);
        
        String temp= "";
        try{
        while (r.next()){
            temp = temp + "<div class=\"menuItem\">\n" + 
		   "<h3>"+ r.getString("M_Name")+ "</h3>\n"+
                   r.getString("M_Description")+ "<br><br>\n"+
                   "<div align='right'>$"+ (double)((int)(Float.parseFloat(r.getString("M_Price"))*100))/100+ "</div>\n"+
                   "</div>\n";
        }
        }catch (Exception e){System.out.println(e);}
        
        temp= temp+ "</span>";
        session.setAttribute("menuView", temp);
        response.sendRedirect("menuview.jsp");
        
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
