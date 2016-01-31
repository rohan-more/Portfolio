/*
 * MenuInfoBean.java
 *
 * Created on October 6, 2004, 10:23 PM
 * Modified on October 6, 2004, 11:03 PM
 */

package intracupojava;

import java.sql.*;
/**
 *
 * @author  Erik Toussaint
 */
public class MenuInfoBean {
    /** Bean variables */
    private String id, name, price, availability, category;
    private String description, ingredients, special;
 
    DBFunctions db = new DBFunctions();
    
    /** Creates a new instance of accountInfoBean */
    public MenuInfoBean() {
    }
    
    /** Accessor Method for Menu ID */
    public String getId() {
        return StringEditor.nullEraser(id);
    }
    
    /** Accessor Method for Menu Name */
    public String getName() {
        return StringEditor.nullEraser(name);
    }
    
    /** Accessor Method for Menu Price */
    public String getPrice() {
        return StringEditor.nullEraser(price);
    }
    
    /** Accessor Method for Menu Availability */
    public String getAvailability() {
        return StringEditor.nullEraser(availability);
    }
    
    /** Accessor Method for Menu Category */
    public String getCategory() {
        return StringEditor.nullEraser(category);
    }
    
    /** Accessor Method for Menu Description */
    public String getDescription() {
        return StringEditor.nullEraser(description);
    }
    
    /** Accessor Method for Menu Ingredients */
    public String getIngredients() {
        return StringEditor.nullEraser(ingredients);
    }
    
    /** Accessor Method for Menu Special */
    public String getSpecial() {
        return StringEditor.nullEraser(special);
    }
  
    
   /** Mutator Method for Menu ID */
    public void setId(){
        id= ""+ name.hashCode();
    }
    
    /** Mutator Method for Menu ID */
    public void setId(String newId){
        id= newId;
    }
    
    /** Mutator Method for Menu Name */
    public void setName(String newName){
        name= newName;
    }
    
    /** Mutator Method for Menu Price */
    public void setPrice(String newPrice){
        price= newPrice;
    }
    
    /** Mutator Method for Menu Availability */
    public void setAvailability(String newAvailability){
        availability= newAvailability;
    }
    
    /** Mutator Method for Menu Category */
    public void setCategory(String newCategory){
        category= newCategory;
    }
    
    /** Mutator Method for Menu Description */
    public void setDescription(String newDescription){
        description= StringEditor.nullEraser(newDescription);
    }
    
    /** Mutator Method for Menu Ingredients */
    public void setIngredients(String newIngredients){
        ingredients= StringEditor.nullEraser(newIngredients);
    }
    
    /** Mutator Method for Menu Special */
    public void setSpecial(String newSpecial){
        special= StringEditor.nullEraser(newSpecial);
    }
  
    
    /** If Menu Category is not valid this outputs <span class="red">*</span> */
    public String flagCategory(){
        String temp= "";
        if (this.getCategory()== null || this.getCategory().equals(""))
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** If Menu Name is not valid this outputs <span class="red">*</span> */
    public String flagName(){
        String temp= "";
        if (this.getName()== null || this.getName().equals(""))
            temp= "<span class='red'>*</span>";
        
        return temp;
    }
    
    /** If Menu ID is not valid this outputs <span class="red">*</span>.
        This method should be used if ID is checked for presence in database
        so it doesnt allow a duplicate row
     */
    public String flagId(){
        String temp= "";
        db.connectDB();
        String query= "SELECT * FROM Menu_Items WHERE M_ID='" +
                        this.getId()+ "'";
        
        ResultSet r = db.get_query(query);
        try{
        if (r.next()== true)
            temp= "<span class=\"red\">*</span>";
        }catch(Exception e){System.out.println(e);}
        
        return temp;
    }
    
    /** If Menu Price is not valid this outputs <span class="red">*</span> */
    public String flagPrice(){
        String temp= "";
        if (this.getPrice()== null || this.getPrice().equals(""))
            temp= "<span class='red'>*</span>";
       
        if (!StringEditor.isDouble(this.getPrice()))
            temp= "<span class='red'>*</span>";
        return temp;
    }
    
    public void reset(){
        id= "";
        name= "";
        price= "";
        availability= "";
        category= "";    
        description= "";
        ingredients= ""; 
        special= "";
    }
 
}
