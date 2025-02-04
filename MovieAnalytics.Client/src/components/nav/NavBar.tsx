import { useAuth } from '@/context/AuthContext';
import { Menu, X } from 'lucide-react';
import { useState } from 'react';

interface NavItem {
  label: string;
  href: string;
}

interface NavBarProps {
  brandName?: string;
  items?: NavItem[];
  logoSrc?: string;
}

const defaultNavItems: NavItem[] = [
  { label: 'Home', href: '/' },
  { label: 'About', href: '/about' },
  { label: 'Services', href: '/services' },
  { label: 'Contact', href: '/contact' },
];

const NavBar = ({
  brandName = 'Your Brand',
  items = defaultNavItems,
  logoSrc
}: NavBarProps) => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const { isAuthenticated, logout, user } = useAuth();


  const toggleMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  const handleLogout = () => {
    logout();
    // You might want to redirect here
  };

  return (
    <nav className="fixed top-0 left-0 right-0 z-50 bg-background border-b border-border">
      <div className="max-w-6xl mx-auto px-4">
        <div className="flex justify-between items-center h-16">
          {/* Brand/Logo Section */}
          <div className="flex items-center">
            {logoSrc && (
              <img
                src={logoSrc}
                alt="Logo"
                className="h-8 w-auto mr-3"
              />
            )}
            <span className="text-xl font-semibold text-foreground">
              {brandName}
            </span>
          </div>

          {/* Desktop Navigation */}
          <div className="hidden md:flex items-center space-x-8">
            {items.map((item) => (
              <a
                key={item.label}
                href={item.href}
                className="text-muted-foreground hover:text-foreground px-3 py-2 rounded-md text-sm font-medium transition-colors"
              >
                {item.label}
              </a>
            ))}
            {isAuthenticated ? (
              <>
                <span className="text-muted-foreground">
                  Welcome, {user?.knownAs}
                </span>
                <button
                  onClick={handleLogout}
                  className="text-muted-foreground hover:text-foreground px-3 py-2 rounded-md text-sm font-medium transition-colors"
                >
                  Logout
                </button>
              </>
            ) : (
              <a
                href="/profile"
                className="text-muted-foreground hover:text-foreground px-3 py-2 rounded-md text-sm font-medium transition-colors"
              >
                Login
              </a>
            )}
          </div>

          {/* Mobile Menu Button */}
          <div className="md:hidden">
            <button
              onClick={toggleMenu}
              className="inline-flex items-center justify-center rounded-md text-muted-foreground hover:text-foreground focus:outline-none focus:ring-2 focus:ring-ring focus:ring-offset-2 focus:ring-offset-background p-2"
              aria-expanded={isMenuOpen}
              aria-label="Toggle navigation menu"
            >
              {isMenuOpen ? <X size={24} /> : <Menu size={24} />}
            </button>
          </div>
        </div>

        {/* Mobile Navigation */}
        {isMenuOpen && (
          <div className="md:hidden">
            <div className="px-2 pt-2 pb-3 space-y-1">
              {items.map((item) => (
                <a
                  key={item.label}
                  href={item.href}
                  className="block text-muted-foreground hover:text-foreground hover:bg-accent px-3 py-2 rounded-md text-base font-medium"
                >
                  {item.label}
                </a>
              ))}
              {isAuthenticated ? (
                <>
                  <span className="block px-3 py-2 text-muted-foreground">
                    Welcome, {user?.knownAs}
                  </span>
                  <button
                    onClick={handleLogout}
                    className="text-muted-foreground hover:text-foreground px-3 py-2 rounded-md text-sm font-medium transition-colors"
                  >
                    Logout
                  </button>
                </>
              ) : (
                <a
                  href="/profile"
                  className="block text-muted-foreground hover:text-foreground hover:bg-accent px-3 py-2 rounded-md text-base font-medium"
                >
                  Login
                </a>
              )}

            </div>
          </div>
        )}
      </div>
    </nav >
  );
};

export default NavBar;