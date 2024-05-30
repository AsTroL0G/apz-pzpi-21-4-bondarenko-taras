import "./styles/main.css"; 
import NavBar from "./Components/navbar/NavBar"
import MainPge from "./pages/MainPage/MainPage"
import Login from "./pages/Login/Login"
import Cabinet from "./pages/Cabinet/Cabinet"
import Register from "./pages/Register/Register"
import {BrowserRouter as Router,Routes,Route} from "react-router-dom";
import IncomingCargo from "./pages/IncomingCargo/IncomingCargo";
import OutgoingCargo from "./pages/OutgoingCargo/OutgoingCargo";
import Cargo from "./pages/Cargo/Cargo";
import CreateCargo from "./pages/CreateCargo/CreateCargo";
import Logist from "./pages/Logist/Logist";
import Statistic from "./pages/Statistic/Statistic";
import Administrator from "./pages/Administrator/Administrator";
function App() {

  const cors = require('cors');
  
  return (
    
    <div className="App">
      <Router>
             <Routes>
              <Route path="/register" element={<Register/>}/>
              <Route path="/login" element={<Login/>}/>
              <Route path="/cargo/:id" element={<Cargo/>}/>
              <Route path="/" element={<MainPge/>}/>
              <Route path="/cabinet" element={<Cabinet/>}/>
              <Route path="/cabinet/incomingCargo" element={<IncomingCargo/>}/>
              <Route path="/cabinet/outgoingCargo" element={<OutgoingCargo/>}/>
              <Route path="/cabinet/createCargo" element={<CreateCargo/>}/>
              <Route path="/cabinet/statistic" element={<Statistic/>}/>
              <Route path="/admin" element={<Administrator/>}/>
              <Route path="/cabinet/logist" element={<Logist/>}/>
          </Routes>
      </Router>
    </div>
  );
}

export default App;
