import { useLocation } from 'react-router-dom';

const SSS = () => {
  const { name } = useLocation().state;

  return (
    <div>
      <h1>Hello {name}!</h1>
      {/* Các nội dung khác */}
    </div>
  );
};

export default SSS;