import React, {Fragment} from 'react'
import { Link } from 'react-router-dom'

const Navbar = ({ user }) => {
  return (
    <nav className="navbar bg-dark">
      <h1>
        <Link to="/"><i className="fa fa-address-card"></i> User Management Portal</Link>
      </h1>
      <ul>
        <li><Link to="/register">Register</Link></li>
        <li><Link to="/login">Login</Link></li>      
      </ul>
    </nav>
  )
}

export default Navbar


