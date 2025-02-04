import {
  NavigationMenu,
  NavigationMenuContent,
  NavigationMenuItem,
  NavigationMenuLink,
  NavigationMenuList,
  NavigationMenuTrigger,
  navigationMenuTriggerStyle
} from "@/components/ui/navigation-menu";


import React from 'react';
import { Link } from "react-router-dom";

interface NavItem {
  label: string;
  links: { name: string, href: string }[];
}

interface NavProps {
  items: NavItem[];
}

export const Nav: React.FC<NavProps> = ({ items }) => {
  return (
    <>
      <NavigationMenu>
        <NavigationMenuList>
          {items.map((item, index) => (
            <NavigationMenuItem key={index}>
              <NavigationMenuTrigger>{item.label}</NavigationMenuTrigger>
              <NavigationMenuContent className="border border-red-500 ">
                {item.links.map((link, linkIndex) => (
                  <Link to={link.href} key={linkIndex}>
                    <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                      {link.name}
                    </NavigationMenuLink>
                  </Link>
                ))}
              </NavigationMenuContent>
            </NavigationMenuItem>
          ))}
        </NavigationMenuList>
      </NavigationMenu>
    </>
  );
}