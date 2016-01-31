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
public class MenuManagementServlet extends HttpServlet {
    
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
            String query= "SELECT * FROM Menu_Items ORDER BY M_Category, M_Name";
            
            ResultSet r = db.get_query(query);
            String menulist= "<select name='menulist' size='1'>"
                                + "<option value='0'>Create new menu item</option>";
            while (r.next()){
                menulist+= "<option value='"+ r.getString("M_ID")+"'>"
                            + "Edit/Delete <b>"+ r.getString("M_Name")+ " "
                            + "</b> information</option>";
            }
            menulist+= "</select>";
            session.setAttribute("menulist", menulist);
            }catch(Exception e){System.out.println("99"+ e);}
         
            if (request.getParameter("hidden")!= null 
                && request.getParameter("hidden").equals("y"))
            {       
                try{
                    db.connectDB();
                    String query= "SELECT * FROM Menu_Items WHERE M_ID="
                                  + request.getParameter("menulist");
                    System.out.println("72");
                    ResultSet r = db.get_query(query); 
                    System.out.println("74");
                    if (r.next())
                    {
                        MenuInfoBean menu= new MenuInfoBean();
                        String id= " "+ r.getInt("M_ID");
                        System.out.println("poo"+ id);
                        String name= " "+ r.getString("M_Name");
                        String price= " "+ r.getString("M_Price");
                        String availability= " "+ r.getInt("M_Availability");
                        String category= " "+ r.getString("M_Category");
                        String description= " "+ r.getString("M_Description");
                        //String ingredients= " "+ r.getString("M_Ingredients");
                        //String special= " "+ r.getString("M_Special");         
                        
                        menu.setName(name.trim());
                        menu.setId(id);
                        menu.setPrice(price.trim());
                        menu.setAvailability(availability.trim());
                        menu.setCategory(category.trim());
                        menu.setDescription(description.trim());
                        System.out.println("poo"+ menu.getId()+ menu.getName());
                        //menu.setIngredients(ingredients.trim());
                        //menu.setSpecial(special.trim());
                        session.setAttribute("menu", menu);
                        response.sendRedirect("menuedit.jsp");
                    }
                    
                    else if (request.getParameter("menulist")!= null
                        && request.getParameter("menulist").equals("0"))
                        response.sendRedirect("menusetup2.jsp");
                    
                    //else response.sendRedirect("accountedit2.jsp");   
                }catch (Exception e){System.out.println("74"+ e);}
                
                //response.sendRedirect("accountedit2.jsp");
            }
            else             
                response.sendRedirect("menumanagement.jsp");
        
        
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
