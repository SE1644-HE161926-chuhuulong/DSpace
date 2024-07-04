import { render, screen } from '@testing-library/react';
import App from './App';
import { GoogleOAuthProvider } from '@react-oauth/google';

test('renders learn react link', () => {
  
  render(
    <GoogleOAuthProvider clientId='336699035226-qoon838gf3f1pjgo1idm17mu948vv1u3.apps.googleusercontent.com'>
      <App />
    </GoogleOAuthProvider>
    );
  
});
