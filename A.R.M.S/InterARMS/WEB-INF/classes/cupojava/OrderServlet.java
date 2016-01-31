/*
 * OrderServlet.java
 *
 * Created on November 21, 2004, 7:17 PM
 */

package cupojava;

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
public class OrderServlet extends HttpServlet {
    
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
        
        int errorStatus= 0;
        db.connectDB();
        String query= "SELECT * FROM Menu_Items WHERE M_Availability > 0";
        
        ResultSet r = db.get_query(query);
        
        int resultsetSize= 0;
        try {
            while (r.next()){
                resultsetSize+= 1;
            }
        }catch (Exception e){System.out.println(e);}
        
        r = db.get_query(query);
        
        int[] id= new int[resultsetSize];
        int[] qty= new int[resultsetSize];
        String[] name= new String[resultsetSize];
        float[] price= new float[resultsetSize];
        
        int rownumber= 0;
        String htmlTable= "<div class=\"order\">\n" +
        "<h1 align=\"center\">Your Order</h1><br>";
        System.out.println("69");
        try{
            while (r.next()){
                System.out.println("72");
                if (request.getParameter(r.getString("M_ID"))== null 
                    || request.getParameter(r.getString("M_ID")).trim().equals(""))
                    ;
                else if (request.getParameter(r.getString("M_ID"))!= null
                && Integer.parseInt(request.getParameter(r.getString("M_ID")))>= 0
                && Integer.parseInt(request.getParameter(r.getString("M_ID")))< 100) {
                    id[rownumber]= Integer.parseInt(r.getString("M_ID"));
                    qty[rownumber]= Integer.parseInt(request.getParameter(r.getString("M_ID")));
                    name[rownumber]= r.getString("M_Name");
                    price[rownumber]= Float.parseFloat(r.getString("M_Price"))*qty[rownumber];
                    
                    htmlTable+=  "<br><b>" + r.getString("M_Name")+ "</b><br>\n" +
                                 "<i>" + r.getString("M_Description") + "</i><br><br>" +
                                 "<div align=\"right\">\n" +
                                 "Unit Price: $" + Float.parseFloat(r.getString("M_Price"))+ "<br>\n"+
                                 "Quantity: " + qty[rownumber] + "<br>\n" +
                                 "Subtotal: $"+ price[rownumber]+ "<br><br></div>\n" +
                                 "<hr>\n";                   
                }
                else {
                    id[rownumber]= -1;
                    qty[rownumber]= -1;
                    name[rownumber]= "";
                    price[rownumber]= 0;
                }
                rownumber+= 1;
            }
        }
        catch (Exception e){
            System.out.println(e);
            //errorStatus= 1;
        }
        
        float totalPrice= 0;
        for (int a= 0; a< price.length; a++)
            totalPrice+= price[a];
        
        if (totalPrice== 0){
            session.setAttribute("pricezero", "1");
            htmlTable+= "<b>YOU DIDN'T MAKE AN ORDER</b>\n";
        }
        
        htmlTable+= "<div align=\"right\"><b><u>TOTAL: $"+ totalPrice+ "</u></b>\n</div></div>";
        
        session.setAttribute("menuOrder", htmlTable);
        if (errorStatus== 1){
            session.setAttribute("menuError", "You can only input numbers greater then 0 and less then 100 in the text fields");
            response.sendRedirect("menu.jsp");
        }
        else
            response.sendRedirect("payment.jsp");
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
