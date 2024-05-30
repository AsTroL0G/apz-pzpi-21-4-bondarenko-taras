import React, { useState, useEffect } from 'react';
import './CookieConsent.css'; // Додайте ваші стилі для компонента

const CookieConsent = () => {
  const [showConsent, setShowConsent] = useState(false);

  // Перевірка наявності згоди у localStorage
  useEffect(() => {
    const consent = localStorage.getItem('cookieConsent');
    if (!consent) {
      setShowConsent(true);
    }
  }, []);

  // Обробник згоди
  const handleAccept = () => {
    localStorage.setItem('cookieConsent', 'true');
    setShowConsent(false);
  };

  if (!showConsent) return null;

  return (
    <div className="cookie-consent">
      <p>Ми використовуємо файли cookie для поліпшення вашого досвіду на нашому сайті. Приймаючи, ви погоджуєтеся з нашою політикою конфіденційності.</p>
      <button onClick={handleAccept}>Прийняти</button>
    </div>
  );
};

export default CookieConsent;
