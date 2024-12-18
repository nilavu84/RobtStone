<%@ Title="Home Page" Language="C#" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Surplus Management System</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link rel="stylesheet" href="styles.css" type="text/css" />
    <link href="StyleSheet_new.css" rel="stylesheet" />
    <style>
        /* Global Styles */
        body {
            font-family: 'Arial', sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
            color: #333;
        }

        a {
            text-decoration: none;
            color: inherit;
        }

        /* Header Styles */
        .header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 10px 20px;
            background-color: #006699;
            color: white;
            border: none;
        }

        .header img {
            height: 50px;
        }

        .header .logout-button {
            background-color: brown;
            color: white;
            border: none;
            padding: 8px 16px;
            font-size: 14px;
            border-radius: 4px;
            cursor: pointer;
            transition: 0.3s ease;
        }

        .header .logout-button:hover {
            background-color: #d32f2f;
        }

        /* Navigation Menu Styles */
        .menu-bar {
            background-color: #0289cc;
            display: flex;
            justify-content: left;
            padding: 10px 0;
        }

        .menu-bar a {
            color: white;
            padding: 10px 15px;
            margin: 0 5px;
            font-size: 16px;
            font-weight: bold;
            border-radius: 4px;
            transition: 0.3s ease;
        }

        .menu-bar a:hover {
            background-color: #006699;
        }

        /* Form Header */
        .form-header {
            text-align: center;
            font-size: 2rem;
            color: #006699;
            margin-top: 20px;
        }

        /* Footer */
        .footer {
            text-align: center;
            padding: 10px;
            background-color: #0289cc;
            color: white;
            position: fixed;
            bottom: 0;
            width: 100%;
        }
        .username-label {
        font-size: 16px;
        color: white;
        margin-right: 20px;
    }
        .logo {
    font-size: 24px;
    font-weight: bold;
    text-transform: uppercase;
    letter-spacing: 2px;
    color: #FFD700; /* Golden accent color */
}
    </style>
    <style>
    .clock {
        width: 200px;
        height: 200px;
        border: 5px solid #333;
        border-radius: 50%;
        position: relative;
        margin: auto;
        background: #f9f9f9;
    }

    .clock div {
        position: absolute;
        width: 50%;
        height: 6px;
        background: #333;
        transform-origin: 100%;
        transform: rotate(90deg);
        transition: transform 0.5s ease-in-out;
    }

    .hour-hand {
        height: 8px;
        background: #111;
        top: 50%;
        left: 50%;
        transform-origin: 100%;
    }

    .minute-hand {
        height: 4px;
        background: #444;
        top: 50%;
        left: 50%;
        transform-origin: 100%;
    }

    .second-hand {
        height: 2px;
        background: red;
        top: 50%;
        left: 50%;
        transform-origin: 100%;
    }
</style>

</head>
<body>
    <form id="form1" runat="server">
        <!-- Header -->
        
       
                <table width="100%"><tr class="header">
    <td><img src="Images/RSME%20Logo_14%20(141x41).png" alt="Logo" /></td>
    <td align="center"><div class="logo">Surplus Management System</div></td>
    <td><asp:Label ID="lblUsername" runat="server" CssClass="username-label"></asp:Label>
        <asp:LinkButton ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" 
        CssClass="logout-button" OnClientClick="return confirm('Are you sure you want to log out?');" /></td>
            </tr>
</table>
        

        <!-- Page Title -->
        <%--<div class="form-header">Surplus Management System</div>--%>

        <!-- Navigation Menu -->
        <div class="menu-bar">
    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" CssClass="menu-bar">
                <Items>
                    <%--<asp:MenuItem Text="Home" Value="Home" NavigateUrl="~/Home.aspx" />--%>
                    <asp:MenuItem Text="Item Entry Form" Value="Registration" NavigateUrl="~/Home.aspx"  />                   
                </Items>
    </asp:Menu>
            <asp:Menu ID="Menu3" runat="server" Orientation="Horizontal" CssClass="menu-bar">
        <Items>
            <asp:MenuItem Text="Item Issued" Value="itemissued" NavigateUrl="~/Itemissued.aspx" />
        </Items>
    </asp:Menu>
    <asp:Menu ID="Menu2" runat="server" Orientation="Horizontal" CssClass="menu-bar">
            <Items>
                <asp:MenuItem Text="Availablity Status Report" Value="viewpage" NavigateUrl="~/Searchpage.aspx" />
            </Items>
    </asp:Menu>
  
 <asp:Menu ID="Menu4" runat="server" Orientation="Horizontal" CssClass="menu-bar">
         <Items>
             <asp:MenuItem Text="Issued Register" Value="viewissuedreport" NavigateUrl="~/IssueViewform.aspx" />
         </Items>
 </asp:Menu>
            <asp:Menu ID="Menu5" runat="server" Orientation="Horizontal" CssClass="menu-bar">
        <Items>
            <asp:MenuItem Text="Issued Revoke" Value="IssuedRevoke" NavigateUrl="~/Revoke.aspx" />
        </Items>
</asp:Menu>

        </div>
        <br />
        <div width="100%">
            <tr>
                <td align="right">
                    <asp:Label ID="lblClock" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>


        </div>

        <!-- Content Section -->
        <div class="content" style="padding: 20px;">
            <!-- Add your content here -->
        </div>

        <!-- Footer -->
        <div class="footer">
            &copy; 2024 ROBT STONE. All rights reserved.
        </div>
    </form>
    <script>
    var idleTime = 0;

    // Increment the idle time counter every minute
    var idleInterval = setInterval(timerIncrement, 60000); // 1 minute

    // Reset the idle timer on user activity
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;
    document.onscroll = resetTimer;
    document.onclick = resetTimer;

    function timerIncrement() {
        idleTime++;
        if (idleTime >= 10) { // 10 minutes
            // Redirect to logout page
            window.location.href = 'Logout.aspx';
        }
    }

    function resetTimer() {
        idleTime = 0;
    }
    </script>
    <script>
        function initializeClock(serverTime) {
            // Parse server time
            const serverDateTime = new Date(serverTime);

            // Function to update the clock every second
            function updateClock() {
                const clockLabel = document.getElementById('<%= lblClock.ClientID %>');

                // Add 1 second to the current time
                serverDateTime.setSeconds(serverDateTime.getSeconds() + 1);

                // Format time as hh:mm:ss AM/PM
                const hours = serverDateTime.getHours();
                const minutes = serverDateTime.getMinutes().toString().padStart(2, '0');
                const seconds = serverDateTime.getSeconds().toString().padStart(2, '0');
                const ampm = hours >= 12 ? 'PM' : 'AM';
                const formattedTime = `${hours % 12 || 12}:${minutes}:${seconds} ${ampm}`;

                // Update the label
                clockLabel.innerText = formattedTime;
            }

            // Start the clock
            updateClock(); // Update immediately
            setInterval(updateClock, 1000); // Update every second
        }
    </script>
    <script>
        function animateClock() {
            const hourHand = document.getElementById('hourHand');
            const minuteHand = document.getElementById('minuteHand');
            const secondHand = document.getElementById('secondHand');

            function updateClock() {
                const now = new Date();
                const hours = now.getHours();
                const minutes = now.getMinutes();
                const seconds = now.getSeconds();

                // Calculate degrees for each hand
                const hourDegree = (hours % 12) * 30 + (minutes / 2); // 360° / 12 = 30° per hour
                const minuteDegree = minutes * 6; // 360° / 60 = 6° per minute
                const secondDegree = seconds * 6; // 360° / 60 = 6° per second

                // Set rotation
                hourHand.style.transform = `rotate(${hourDegree}deg)`;
                minuteHand.style.transform = `rotate(${minuteDegree}deg)`;
                secondHand.style.transform = `rotate(${secondDegree}deg)`;
            }

            // Update clock immediately
            updateClock();

            // Update every second
            setInterval(updateClock, 1000);
        }

        // Start the clock animation
        window.onload = animateClock;

        function animateClock(serverTime) {
            const now = serverTime ? new Date(serverTime) : new Date();

            // Remainder of the code stays the same
        }

    </script>


</body>
</html>
