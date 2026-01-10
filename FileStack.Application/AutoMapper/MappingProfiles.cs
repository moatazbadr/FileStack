using AutoMapper;
using FileStack.Application.DTOS;
using FileStack.Application.Folders.Commands.CreateFolder;
using FileStack.Application.Folders.Commands.RenameFolder;
using FileStack.Domain.Entities;

namespace FileStack.Application.AutoMapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Add your mappings here 
        CreateMap<CreateFolderDto ,Folder>()
            .ReverseMap();

        CreateMap<CreateFolderDto, CreateFolderCommand>().ReverseMap();
        CreateMap<RenameFolderDto, RenameFolderCommand>().ReverseMap();
    }
}
