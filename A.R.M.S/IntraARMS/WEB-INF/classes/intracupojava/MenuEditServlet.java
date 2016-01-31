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
public class MenuEditServlet extends HttpServlet {
    
    DBFunctions db = new DBFunctions();
    /** Initializes Beans and variables.
     */
    public MenuInfoBean menu;
    
    
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
        String tempID= "";
        //initialize account bean
        if (session.isNew()== true || session.getAttribute("menu")== null)
            menu= new MenuInfoBean();
        else if (session.isNew()== false && session.getAttribute("menu")!= null)
            menu= (MenuInfoBean)session.getAttribute("menu");
        else 
            menu= new MenuInfoBean();
        
        tempID= menu.getId();
        
        try{
        
        menu.setName(StringEditor.removeBadCharsfromString(request.getParameter("name")));
        menu.setPrice(request.getParameter("price"));
        menu.setDescription(StringEditor.removeBadCharsfromString(request.getParameter("description")));
        menu.setId();
        //System.out.println(request.getParameter("availability"));
        if (request.getParameter("availability")!= null 
            && request.getParameter("availability").equals("on"))
            menu.setAvailability("1");
        else if (request.getParameter("availability")!= null
                && request.getParameter("availability").equals("off"))
            menu.setAvailability("0");
        else 
            menu.setAvailability("0");
        System.out.println("Success");
        } catch (Exception e){System.out.println("11"+ e.getMessage());}
        
        
        session.setAttribute("menu", menu);
 
        db.connectDB();
        String query= "SELECT * FROM Menu_Items WHERE M_ID="
        + menu.getId();
        
        if (request.getParameter("action")!= null 
        && request.getParameter("action").equals("Delete"))
        {
            query= "DELETE FROM Menu_Items WHERE M_ID="
            + menu.getId();
            db.update_query(query);
            String temp= "You successfully deleted <b>"+ menu.getName()
                    + "</b> from menu";
            session.setAttribute("acceditsuccess", temp);
            response.sendRedirect("action.jsp");
        }
       
        else if(request.getParameter("action")!= null 
        && request.getParameter("action").equals("   Edit   ")){
        if (menu.flagName().equals("") && menu.flagPrice().equals("")){
        
            db.connectDB();
        try{
            query= "UPDATE Menu_Items "+
            "SET M_ID="+menu.getId()+" , "+ 
            "M_Name='"+menu.getName().toUpperCase()+"' , "+
            "M_Description='"+menu.getDescription()+"' , "+
	    "M_Price="+menu.getPrice()+ " , "+
            "M_Availability="+menu.getAvailability()+" "+
            "WHERE M_ID="+tempID;
        }catch(Exception e){System.out.println(e);}
            
        System.out.println(query);
        db.update_query(query);
        
        String temp= "You successfully edited <b>"+ menu.getName()
                    + "</b>'s information";
        session.setAttribute("acceditsuccess", temp);
        response.sendRedirect("action.jsp");
            
      }
        
        else{
            response.sendRedirect("menuedit.jsp");
        }
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
