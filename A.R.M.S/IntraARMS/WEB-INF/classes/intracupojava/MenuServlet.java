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
public class MenuServlet extends HttpServlet {
    
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
        
        //initialize account bean
        if (session.isNew()== true || session.getAttribute("menu")== null)
            menu= new MenuInfoBean();
        else if (session.isNew()== false && session.getAttribute("menu")!= null)
            menu= (MenuInfoBean)session.getAttribute("menu");
        
        try{
        menu.setName(StringEditor.removeBadCharsfromString(request.getParameter("name")));
        menu.setDescription(StringEditor.removeBadCharsfromString(request.getParameter("description")));
        menu.setPrice(StringEditor.removeBadCharsfromString(request.getParameter("price")));
        if (request.getParameter("availability").equals("on"))
            menu.setAvailability("1");
        else 
            menu.setAvailability("0");
        menu.setId();
        
        
        } catch (Exception e){System.out.println(e.getMessage());}
       
        
        if (menu.flagName().equals("") && menu.flagPrice().equals("")){
        
            db.connectDB();
            String query= "";
        try{
            query= "INSERT INTO Menu_Items (M_ID, M_Name, M_Description, M_Price, M_Availability)"
            +" VALUES ("+ menu.getId()+ " , '"+menu.getName()+"' , '"
            +menu.getDescription()+"' , "+menu.getPrice()+" , "+ menu.getAvailability()+")";
        }catch(Exception e){System.out.println(e);}
        System.out.println(query);
        db.update_query(query);
        String temp2= "You successfully created the <b>"+ menu.getName()+ " </b>menu item";
        session.setAttribute("menucreatesuccess", temp2);
        response.sendRedirect("action.jsp");
      }
        
        else{
            String temp2= "";
            if (menu.flagId().equals("<span class=\"red\">*</span>")){
                temp2= "This Menu Item Name is already in use, please try another one";
                session.setAttribute("menuinuse", temp2);
            }
            response.sendRedirect("menusetuperror.jsp");
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
