import { Link } from "react-router-dom";
import { sports } from "./sports";
import "./Header.css";

export function Header() {
    return (
        <nav className="sports-header">
            <Link to="/">Home</Link>

            {sports.map((sport) => (
                <div key={sport.path} className="sport-item">
                    <Link to={sport.path}>
                        {sport.name}
                        {sport.leagues.length > 0 && " ▼"}
                    </Link>

                    {sport.leagues.length > 0 && (
                        <div className="sport-dropdown">
                            {sport.leagues.map((league) => (
                                <Link key={league.path} to={league.path} className="dropdown-item">
                                    {league.name}
                                </Link>
                            ))}
                        </div>
                    )}
                </div>
            ))}
        </nav>
    );
}