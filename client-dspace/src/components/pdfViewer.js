import { useNavigate, useParams } from "react-router-dom";
import Header from "./Header/Header";

const PdfViewer = () => {
    const { fileId } = useParams();
    const processedFileId = fileId.split('@')[0];
    const itemId = fileId.split('@')[1];
    const navigate = useNavigate();

    const handleClickBackToItem = (itemId) => {
        navigate(`/DSpace/ItemDetails/${itemId}`);
    };
    return (
        <div>
            <Header />
            <div className="d-flex flex-column" style={{ backgroundColor: "darkkhaki" }}>
                <div className="d-flex align-items-start mt-2 ms-2">
                    <button type="button" className="btn btn-outline-dark" onClick={() => handleClickBackToItem(itemId)}>Back to Item</button>
                </div>

                <div className="d-flex align-items-center flex-column w-100 mt-2 " >
                    <iframe src={`https://drive.google.com/file/d/${processedFileId}/preview`} width="40%" height="860px" allow="autoplay"></iframe>
                </div>
            </div>

        </div>

    )
};
export default PdfViewer;
