<div id="top"></div>
<div align="center">
  <h1>Burger Application</h1>
  </br>
  <img src="readme-logo.png" alt="Logo" width="100" height="100">
 
  <h4 align="center">Desktop application for burger restaurant</h4>
 </br>
</div>
</br>

<p>🟢 TABLE OF CONTENTS 🟢</p>
  <ol>
    <li>
      <a href="#design">Design</a>
    </li>
    <li>
      <a href="#database-structure">Database structure</a>
    </li>
  </ol>
</br>

## Design

<ol>
  <li>
    <p>Log in window</p>
      <ul>
        <li>After employee logs in, the main panel with side menu is displayed</li>
        <li>After Admin logs in, the "Admin panel" button is additionally displayed</li>
      </ul>
  </li>
  <li>
    <p>Navigation menu</p>
      <ul>
        <li>Sales</li>
        <li>Shopping cart</li>
        <li>Raport</li>
        <li>Supplies</li>
        <li>Admin panel</li>
        <li>Log out</li>
      </ul>
  </li>
  <li>
    <p>UI design</p>
    <ol type="a">
      <li>
        <p>Sales window</p>
        <ul>
          <li>Menu with possibility of adding products to shopping cart</li>
          <li>After click on a product, the order customization window opens with possibility of changing ingrediens</li>
          <li>Window with allergens (optional)</li>
        </ul>
      </li>
      <li>
        <p>Shopping cart window</p>
        <ul>
          <li>Summary with final price</li>
          <li>Field for discount code</li>
          <li>Payment type</li>
          <li>"Download invoice PDF" button</li>
        </ul>
      </li>
      <li>
        <p>Raport window</p>
        <ul>
          <li>"Generate sales raport" button in specific period of time</li>
          <li>Chosing a time period (calendar)</li>
          <li>"Download raport PDF" button</li>
        </ul>
      </li>
      <li>
        <p>Supplies window</p>
        <ul>
          <li>Supplies list from specific days (after click on delivery, the view with details is displayed)</li>
          <li>"Add delivery" button (Form to input name, amount and price)</li>
        </ul>
      </li>
      <li>
        <p>Admin panel</p>
        <ul>
          <li>Employee's data edition</li>
          <li>Adding and deleting employee</li>
          <li>Empowerment</li>
          <li>Menu edition</li>
          <li>Adding and deleting regular clients</li>
        </ul>
      </li>
    </ol>
  </li>
</ol>
<p align="right"><a href="#top">🔺 TOP 🔺</a></p>
</br>

## Database structure

<ol>
  <li>
    <p>Products</p>
      <ul>
        <li>Id</li>
        <li>Name</li>
        <li>Price</li>
        <li>Description</li>
      </ul>
  </li>
  <li>
    <p>Allergens</p>
      <ul>
        <li>Id</li>
        <li>Name</li>
      </ul>
  </li>
  <li>
    <p>Products - Allergens</p>
      <ul>
        <li>Id</li>
        <li>Id product</li>
        <li>Id allergen</li>
      </ul>
  </li>
  <li>
    <p>Deliveries</p>
      <ul>
        <li>Id</li>
        <li>Delivery</li>
        <li>Date</li>
      </ul>
  </li>
  <li>
    <p>Staff</p>
      <ul>
        <li>Id</li>
        <li>Login</li>
        <li>Password</li>
        <li>Role</li>
        <li>Forename</li>
        <li>Surname</li>
        <li>Pesel</li>
        <li>Email</li>
        <li>Id address</li>
        <li>Id Burger restaurant</li>
        <li>Account creation date</li>
        <li>Account closure date</li>
      </ul>
  </li>
  <li>
    <p>Addresses</p>
      <ul>
        <li>Id</li>
        <li>City</li>
        <li>Postal code</li>
        <li>Street</li>
        <li>House number</li>
        <li>Flat number (nullable)</li>
      </ul>
  </li>
  <li>
    <p>Regular customers</p>
      <ul>
        <li>Id</li>
        <li>Email</li>
        <li>Code</li>
      </ul>
  </li>
  <li>
    <p>Discount codes</p>
      <ul>
        <li>Id</li>
        <li>Code</li>
        <li>Discount percent</li>
        <li>Amount</li>
        <li>Amount of vouchers (1 use decreases amount of vouchers by 1)</li>
      </ul>
  </li>
  <li>
    <p>Orders</p>
      <ul>
        <li>Id</li>
        <li>Employee Id</li>
        <li>Customer Id (nullable)</li>
        <li>Discount code Id (nullable)</li>
        <li>Sale date</li>
      </ul>
  </li>
  <li>
    <p>Orders - products</p>
      <ul>
        <li>Id</li>
        <li>Order Id</li>
        <li>Product Id</li>
      </ul>
  </li>
    <li>
    <p>Reports</p>
      <ul>
        <li>Id</li>
        <li>Report</li>
        <li>Date</li>
      </ul>
  </li>
  <li>
    <p>Burger restaurants</p>
      <ul>
        <li>Id</li>
        <li>Id address</li>
      </ul>
  </li>
</or>
<p align="right"><a href="#top">🔺 TOP 🔺</a></p>
</br>
