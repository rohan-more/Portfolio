/*
 * PaymentServlet.java
 *
 * Created on December 7, 2004, 2:44 PM
 */

package cupojava;

import java.io.*;
import java.net.*;

import javax.servlet.*;
import javax.servlet.http.*;

/**
 *
 * @author  toussainte
 * @version
 */
public class PaymentServlet extends HttpServlet {
    
    DBFunctions db = new DBFunctions();
    /** Initializes Beans and variables.
     */
    public PaymentInfoBean payment;
    
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
        if (session.isNew()== true || session.getAttribute("payment")== null)
            payment= new PaymentInfoBean();
        else if (session.isNew()== false && session.getAttribute("payment")!= null)
            payment= (PaymentInfoBean)session.getAttribute("payment");
        
        try{
        payment.setName(StringEditor.removeBadCharsfromString(request.getParameter("ccname")));
        payment.setNumber(StringEditor.removeBadCharsfromString(request.getParameter("ccno")));
        payment.setCompany(StringEditor.removeBadCharsfromString(request.getParameter("cctype")));
        payment.setExpiration(StringEditor.removeBadCharsfromString(request.getParameter("ccexp")));
        payment.setSecurity(StringEditor.removeBadCharsfromString(request.getParameter("ccsec")));
        
        } catch (Exception e){System.out.println("63"+ e.getMessage());}
        
        
        session.setAttribute("payment", payment);
        
        if (payment.flagCompany().equals("") && payment.flagExpiration().equals("")
        && payment.flagName().equals("") && payment.flagNumber().equals("")
        && payment.flagSecurity().equals(""))
        {
            String query= "";
            
            try{
            query= "INSERT INTO Billing_Information (BI_ID, BI_Card_Name, "
            + "BI_Card_Number, BI_Card_Type, BI_Card_Expiration, BI_Card_Security, "
            + "BI_Acct_ID) VALUES ('" + payment.hashCode()+ "', '"
            + payment.getName()+ "', '"+ payment.getNumber()+ "', '"
            + payment.getCompany()+ "', '"+ (java.sql.Date)payment.expDate()+ "', "
            + Integer.parseInt(payment.getSecurity())+ ", '"
            + session.getAttribute("id")+ "')";
            }catch(Exception e){System.out.println("There was a problem in query line 74");}
            
            System.out.println(query);
            db.update_query(query);
            
            String temp= "Thank You For Ordering With Us <b>"+ 
                    payment.getName().toUpperCase()+ "</b>!";
            session.setAttribute("acceditsuccess", temp);
            response.sendRedirect("action.jsp");
        }
        
        else
        {
            response.sendRedirect("payment.jsp");
        }
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
