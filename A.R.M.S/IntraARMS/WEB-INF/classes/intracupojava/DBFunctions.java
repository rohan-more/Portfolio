/*
 * DBFunctions.java
 *
 * Created on November 10, 2004, 11:04 PM
 */

/**
 *
 * @author  Rich
 */
package intracupojava;
import java.lang.String;
import java.sql.*;
import com.sybase.jdbc.*;
import com.sybase.jdbcx.SybDriver;
import java.util.*;
import javax.swing.table.DefaultTableModel;

public class DBFunctions {
    
    /* Translab Database Info */
    private final String m_dbURL = "jdbc:sybase:Tds:rdb.translab.cs.sunysb.edu:"
      + "5000/c308_DB02";
    private final String m_dbProxy = "http://www.translab.cs.sunysb.edu/proxy/" +
      "servlet/TDSTunnelServlet";
    private final String m_dbUser = "c308db02";
    private final String m_dbPass = "#TkjGtR";
  
    /*current connection*/
    private Connection m_dbConnect;

    //General Member Variables
    private static final boolean DEBUG = false;
    
    
    /** Creates a new instance of TestLogin */    
        public DBFunctions() {
    }
        
        
  public int connectDB()
  {
    try {
      if(m_dbConnect != null) {
        if(!m_dbConnect.isClosed())
          return 0; //Connected, don't re-open
        else
          m_dbConnect = null;
      }
      Properties p = new Properties();
      Class c = Class.forName("com.sybase.jdbc.SybDriver");
      SybDriver sybaseDriver = (SybDriver) c.newInstance();
      DriverManager.registerDriver((Driver)sybaseDriver);

      p.put("user",m_dbUser);
      p.put("password",m_dbPass);
      p.put("proxy", m_dbProxy);

      m_dbConnect = DriverManager.getConnection(m_dbURL,p);
      if(m_dbConnect == null)
        return -1;
      if(m_dbConnect.isClosed()){
        m_dbConnect = null;
        return -1;
      }
    }
    catch(Exception e) {
      System.out.println(e);
      return -1;
    }

    return 0;
  }

  public int disconnectDB()
  {
    try {
      if(m_dbConnect == null)
        return 0;
      if(m_dbConnect.isClosed()) {
        m_dbConnect = null;
        return 0;
      }

      m_dbConnect.close();
      if(!m_dbConnect.isClosed())
        return -1;
      m_dbConnect = null;
      return 0;
    }
    catch(Exception e) {
      System.out.println(e);
      return -1;
    }
  }

  public ResultSet get_query(String query)
  {
    try {
      if(connectDB() != 0) {
        System.out.println("Failed to Connect to DB");
        return null;
      }
      Statement statement = m_dbConnect.createStatement();
      ResultSet result = statement.executeQuery(query);
      if(disconnectDB() != 0)
        System.out.println("Failed to Disconnect from DB!\n");
      return result;
    }
    catch (Exception e) {
      disconnectDB();
      System.out.println(e);
      return null;
    }
  }

  public boolean update_query(String query)
  {
    try {
      if(connectDB() != 0) {
        System.out.println("failed to connect to Database");
        return false;
      }
      Statement statement = m_dbConnect.createStatement();
      statement.execute(query);
      if(disconnectDB() != 0)
        System.out.println("Failed to Disconnect from DB!\n");
      return true;
    }
    catch(Exception e) {
      disconnectDB();
      System.out.println(e);
      return false;
    }
  }    
    
}






/*
  public String removeTrailingSpaces(String str)
  {
    String ret = "";
    int i;
    //Removing Junk at the End (Spaces)
    for(i = str.length()-1;(i >= 0) && (str.charAt(i) == ' ');i--);
    while(i >= 0)
      ret = str.charAt(i--) + ret;
    return ret;
  }

  //fills the JTable with values from a ResultSet


  public void fillTable(JTable table, ResultSet resultSet, String[] headings)
 {
   try{
     DefaultTableModel tempModel = (DefaultTableModel) table.getModel();
     tempModel.setColumnIdentifiers(headings);

     //Clear Rows
     while(tempModel.getRowCount() > 0)
       tempModel.removeRow(0);

     //use RSMD to get # of columns for inner loop
     ResultSetMetaData rsmd = resultSet.getMetaData();
     int colNo = rsmd.getColumnCount();
     //while there is a non-empty value for the next row of the resultSet
     while(resultSet.next()){
       Object[] objects = new Object[colNo];
       //pull each column value out and put it into the Object Array
       for(int x=0;x<colNo;x++){
         objects[x]=resultSet.getObject(x+1);
       }
       //add the row to the table
       tempModel.addRow(objects);
     }
     //set table to a DefaultTableModel filled w/ the data
     table.setModel(tempModel);
     //resize the rows
     table.sizeColumnsToFit(-1);
   }    catch(Exception e) {
     System.out.println(e);
     return;
   }
 }

  public String getCorrectCase(ResultSet result,String str,String column)
  {
    try {
      String ret = null;

      if(result == null)
        return null;
      while(result.next()) {
        if(removeTrailingSpaces(result.getString(column).toUpperCase()).equals(str.toUpperCase())) {
          ret = removeTrailingSpaces(result.getString(column));
          break;
        }
      }
      if(ret == null)
        return null;
      return ret;
    }
    catch (Exception ex) {
      System.out.println(ex);
      return null;
    }
  }

  public String[] getComboString(String query,String column)
  {
    try {
      ResultSet result = get_query(query);
      if(result != null) {
        int rows = 0;
        LinkedList list = new LinkedList();
        while(result.next()) {
          list.addLast(result.getString(column));
          rows++;
        }
        String []str = new String[rows];
        for(int i = 0;i < rows;i++)
          str[i] = removeTrailingSpaces((String)list.get(i));
        return str;
      }
      return new String[0];
    }
    catch(Exception e) {
      System.out.println(e);
      return new String[0];
    }
  }
} */