/*
 * LoginServlet.java
 *
 * Created on November 9, 2004, 2:23 PM
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
public class LoginServlet extends HttpServlet {
    
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
        String query= "SELECT * FROM Account_Information WHERE AI_ID='" +
        request.getParameter("login")+ "' AND AI_Password='" + 
        request.getParameter("password")+ "'";
        
        ResultSet r = db.get_query(query);
        System.out.println(request.getParameter("login")+ " & " + request.getParameter("password"));
        try{
        if (r.next()== true){
            session.setAttribute("permission", r.getString("AI_Type"));
            session.setAttribute("id", r.getString("AI_ID"));
            session.setAttribute("firstName", r.getString("AI_First_Name"));
            response.sendRedirect("action.jsp");
        }
        else{
            
            session.setAttribute("loginError", "Login and/or Password invalid");
            response.sendRedirect("index.jsp");
        }
        }catch (Exception e){System.out.println(e);}
        
        
        
        
        
        
        
        
        
        /*
        try{
            ResultSetMetaData rsmd = r.getMetaData();
            int colNo = rsmd.getColumnCount();
            for(int col = 1; col < colNo; col ++){
                    System.out.print(rsmd.getColumnLabel(col));
                }
            System.out.println();
            while(r.next()){
                for(int col = 1; col < colNo; col ++){
                    System.out.print(r.getString(col));
                }
                System.out.println();
            }
            }    catch(Exception e) {
                System.out.println("Damn!" + e);
                    return;
        }*/
        
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
