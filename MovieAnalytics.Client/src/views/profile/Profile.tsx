import { useAuth } from '@/context/AuthContext';
import React from 'react';
import { LoginForm } from '../auth/LoginForm';

interface ProfileProps {}

export const Profile: React.FC<ProfileProps> = () => {
  const { isAuthenticated, user } = useAuth();

  if (!isAuthenticated) {
    return <LoginForm />;
  }

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">
        Welcome to Profile, {user?.knownAs}!
      </h1>
      <div className="bg-card rounded-lg shadow p-6">
        <div className="space-y-4">
          <div>
            <p className="text-muted-foreground">Username:</p>
            <p className="font-medium">{user?.username}</p>
          </div>
          <div>
            <p className="text-muted-foreground">User ID:</p>
            <p className="font-medium">{user?.id}</p>
          </div>
        </div>
      </div>
    </div>
  );
};